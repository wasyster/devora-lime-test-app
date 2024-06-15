<script setup>
  import 'element-plus/theme-chalk/display.css';
  import { RouterLink } from 'vue-router';
  import { store } from './infrastructure/store';

  const router = useRouter();
  const state = store.state;

  store.state.loading = false;
</script>

<template>
  <el-container class="el-container">
    <el-main v-loading="state.loading">
      <suspense>
        <router-view v-slot="{ Component, route }">
          <transition name="fade" mode="out-in">
            <component :is="Component" :key="route.path" />
          </transition>
        </router-view>
      </suspense>
    </el-main>
  </el-container>
</template>

<style scoped>
.fade-enter-active,
.fade-leave-active {
  transition: opacity 0.3s;
}

.fade-enter,
.fade-leave-to {
  opacity: 0;
}
</style>
