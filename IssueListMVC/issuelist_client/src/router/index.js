import Vue from 'vue'
import VueRouter from 'vue-router'
import Home from '../views/Home.vue'
import Login from '../views/Login.vue'

Vue.use(VueRouter)

//guard for routes
function guardMyroute(to, from, next) {
  var isAuthenticated = false;

  var token = JSON.parse(localStorage.getItem('token'));
  if (token && token.expirationDate > Date.now() && token.access_token)
    isAuthenticated = true;
  else
    isAuthenticated = false;
  if (isAuthenticated) {
    Vue.notify({
      title: 'Authorization',
      text: 'You have been logged in!',
      type:'success'
    })
    next(); // allow to enter route
  }
  else {
    next('/login'); // go to '/login';
    Vue.notify({
      title: 'Authorization',
      text: 'You do not have access to this page, please log in',
      type:'error'
      
    })
  }
}

const routes = [
  {
    path: '/',
    name: 'Home',
    component: Home,
    beforeEnter: guardMyroute,
    meta: { title: 'Home' }
  },
  {
    path: '/login',
    name: 'Login',
    component: Login,
  }
]

const router = new VueRouter({
  mode: 'history',
  base: process.env.BASE_URL,
  routes
})

export default router
