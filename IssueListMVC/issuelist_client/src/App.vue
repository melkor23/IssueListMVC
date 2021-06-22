<template>
  <div id="app">
    <notifications />

    <div>
      <b-navbar class="d-flex justify-content-end pr-3 " type="dark" variant="dark">
        <b-navbar-nav>
          <!-- Language -->
          <b-nav-item-dropdown text="Lang" >
            <b-dropdown-item
              v-for="entry in languages"
              :key="entry.title"
              @click="changeLocale(entry.language)" >
              {{ entry.title }}
            </b-dropdown-item>
          </b-nav-item-dropdown>

          <b-nav-item-dropdown v-bind:text="username" style="margin-right: 15px;" >
            <b-dropdown-item @click="logOut()" href="#">{{$t('logOutButton')}}</b-dropdown-item>
          </b-nav-item-dropdown>

        </b-navbar-nav>
      </b-navbar>
    </div>

    <router-view class="pt-3" />
  </div>
</template>

<script>
import i18n from "@/plugins/i18n";

export default {
  name: "App",
  components: {},
  data() {
    return {
      username: "",
      languages: [
        { flag: "us", language: "en", title: "English" },
        { flag: "es", language: "es", title: "Español" },
      ],
    };
  },
  methods: {
    logOut() {
      localStorage.removeItem("token");
      this.isUserLogued = false;
      this.$router.push("/login");
    },
    changeLocale(locale) {
      i18n.locale = locale;
    },
  },
};
</script>

<style>
#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
}

#nav {
  padding: 30px;
}

#nav a {
  font-weight: bold;
  color: #2c3e50;
}

#nav a.router-link-exact-active {
  color: #42b983;
}
</style>
