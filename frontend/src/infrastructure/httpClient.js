import { store } from './store';

export const httpClient = {
  apiUrl() {
    if (import.meta.env.MODE === 'development') return import.meta.env.VITE_API_URL;

    return `https://${window.location.host}`;
  },

  /**
   * Creates an async HTTP GET request to the specified URL (awaitable).
   * @param {string} url The url of the call
   * @param {object=} data The data if any
   * @param {boolean=} [useCache=true] Do we want to cache the response? Default is true.
   * @returns {Promise<object>} The response object from json.
   */
  async getAsync(url, data, useCache = true) {
    const endpointUrl = data
      ? `${this.apiUrl()}/${url}${this._serializeToQueryStringParams(data)}`
      : `${this.apiUrl()}/${url}`;

    if (useCache) {
      return await this.cachedGetJsonAsync(endpointUrl);
    } 
    else {
      const result = await fetch(endpointUrl, {
        method: 'GET',
        cache: 'no-cache',
        headers: this._createHeaders(),
    });

    return {
        success: result.ok,
        data: await result.json()
      };
    }
  },

  async postAsync(url, data, useCache = true, hasResponseConcent = true) {
    const result = await fetch(`${this.apiUrl()}/${url}`, {
      method: 'POST',
      cache: 'no-cache',
      headers: this._createHeaders(),
      body: JSON.stringify(data), // body data type must match "Content-Type" header
    });

    return {
      success: result.ok,
      data: hasResponseConcent ? await result.json() : undefined
    };
  },

  async getPdfFileAsync(url, data, fileName) {
    const result = await fetch(`${this.apiUrl()}/${url}`, {
      method: 'POST',
      cache: 'no-cache',
      headers: this._createHeaders(),
      body: JSON.stringify(data), // body data type must match "Content-Type" header
    });

    if(!result.ok) {
      return;
    }

    const blob = await result.blob();
    var contentType = { 'application/pdf': ['.pdf'] };
    this.downloadFile(blob, `${fileName}.pdf`, contentType);
  },

  async uploadFileAsync(url, file) {
    const data = new FormData();
    data.append('image', file);
    data.append('name', file.name);

    const result = await fetch(`${this.apiUrl()}/${url}`, {
      method: 'POST',
      cache: 'no-cache',
      headers: this._createHeaders(false),
      body: data, // body data type must match "Content-Type" header
    });

    return await result.json();
  },

  async uploadFileToSignedUrlAsync(url, file) {
    const data = new FormData();
    data.append('image', file);
    data.append('name', file.name);

    await fetch(url, {
      method: 'PUT',
      cache: 'no-cache',
      headers: {
        "x-ms-blob-type": "BlockBlob",
        'Content-Type': file.type,
      },
      body: data, // body data type must match "Content-Type" header
    });
  },

  async cachedGetJsonAsync(url) {
    // Use the URL as the cache key to sessionStorage
    const cacheKey = this._hashString(url);
    let cached = sessionStorage.getItem(cacheKey);
    if (cached !== null) {
      let response = new Response(new Blob([cached]));
      return {
        success: true,
        data: await response.json()
      };
    }

    const response = await fetch(url, {
      method: 'GET',
      cache: 'no-cache',
      headers: this._createHeaders()
    });

    // let's only store in cache if the content-type is JSON
    if (response.status === 200) {
      let ct = response.headers.get('Content-Type');
      if (ct && ct.match(/application\/json/i)) {
        // There is a .json() instead of .text() but
        // we're going to store it in sessionStorage as
        // string anyway.
        // If we don't clone the response, it will be
        // consumed by the time it's returned. This
        // way we're being un-intrusive.
        const content = await response.clone().text();
        sessionStorage.setItem(cacheKey, content);
      }
    }

    return {
      success: response.ok,
      data: await response.json()
    };
  },

  clearCache(url) {
    const absoluteUrl = `${this.apiUrl()}/${url}`;
    const cacheKey = this._hashString(absoluteUrl);
    sessionStorage.removeItem(cacheKey);
  },

  _createHeaders(sendContentType = true) {
    const headers = new Headers();
    if (sendContentType) {
      headers.append('Content-Type', 'application/json');
    }

    return headers;
  },

  _serializeToQueryStringParams(obj) {
    let str = [];
    for (let p in obj) {
      if (obj.hasOwnProperty(p)) {
        const parameter = obj[p];
        if (Array.isArray(parameter)) {
          str = Object.entries(obj)
            .map(s => s[1].map(e => `${encodeURIComponent(s[0])}=${encodeURIComponent(e)}`))
            .flat()
            .join('&');
          return '?' + str;
        } else {
          str.push(encodeURIComponent(p) + '=' + encodeURIComponent(parameter));
          return '?' + str.join('&');
        }
      }
    }
  },

  _hashString(s) {
    let hash = 0;
    if (s.length === 0) return hash;
    for (let i = 0; i < s.length; i++) {
      let char = s.charCodeAt(i);
      hash = (hash << 5) - hash + char;
      hash = hash & hash; // Convert to 32bit integer
    }
    return hash;
  },

  async downloadFile(blob, fileName, contentType) {
    var file = new Blob([blob], {type: contentType});
    if (window.navigator.msSaveOrOpenBlob) // IE10+
        window.navigator.msSaveOrOpenBlob(file, fileName);
    else { // Others
        var a = document.createElement("a");
        var url = URL.createObjectURL(file);
        a.href = url;
        a.download = fileName;
        document.body.appendChild(a);
        a.click();
        setTimeout(function() {
            document.body.removeChild(a);
            window.URL.revokeObjectURL(url);  
        }, 0); 
    }
  }
};
