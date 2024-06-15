import { createRouter, createWebHistory } from 'vue-router';
import App from './App.vue';

import './style.css';
import './assets/css/main.css';
import 'element-plus/dist/index.css';

const index = () => import('./pages/index/Index.vue');
const notFound = () => import('./pages/notFound/NotFound.vue');

const routes = [
  { path: '/', component: index },
  { path: '/:catchAll(.*)', component: notFound },
];
const router = createRouter({
  history: createWebHistory(),
  routes
});

const app = createApp(App)
            .use(router);

app.mount('#app')