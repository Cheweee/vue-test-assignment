import Vue from 'vue'
import VueRouter from 'vue-router'
import VueMaterial from 'vue-material';
import App from './App.vue'

import 'vue-material/dist/vue-material.min.css'
import 'vue-material/dist/theme/default.css'

Vue.config.productionTip = false;
Vue.use(VueMaterial);
Vue.use(VueRouter);

const routes = [
  { 
    path: '/', 
    component: App
  }
]

const router = new VueRouter({
  routes,
  mode: 'history'
})

new Vue({
  el: '#app',
  template: "<div style='height:100%;'><router-view></router-view></div>",
  router
})