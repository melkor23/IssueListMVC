import Vue from 'vue'
import App from './App.vue'
import router from './router'
import axios from "axios";
//bootstrap
import { BootstrapVue, BootstrapVueIcons } from 'bootstrap-vue'
import 'bootstrap-vue/dist/bootstrap-vue.css';
import 'bootstrap/dist/css/bootstrap.css';
//i18n
import i18n from '@/plugins/i18n';
import development from "@/config/development.json";
import production from "@/config/production.json";
//notifications
import Notifications from 'vue-notification'

Vue.use(BootstrapVue);
Vue.use(BootstrapVueIcons);
Vue.use(Notifications);

Vue.config.productionTip = false


//interceptor add jwt token
axios.interceptors.request.use(function (config) {
  const token = JSON.parse(localStorage.getItem('token')).access_token;
  if (token) {
    config.headers.Authorization = `Bearer ${token}`;
  }
  return config;
});

//interceptor token expired && http erors
axios.interceptors.response.use(response => {
  return response;
}, error => {
  if (error.response.status === 401) {
    this.$router.push("/login");
  } else {
    Vue.notify({
      title: "Http request error",
      text: error.response,
      type: "error",
    });
  }
});

//get config file
if (process.env.NODE_ENV === "production") {
  Vue.prototype.$config = Object.freeze(production);
} else {
  Vue.prototype.$config = Object.freeze(development);
}


new Vue({
  i18n,
  router,
  render: h => h(App)
}).$mount('#app')
