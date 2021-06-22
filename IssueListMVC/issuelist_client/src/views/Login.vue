<template>
  <div class="home">
    <div class="d-flex justify-content-center" v-show="!isUserLogued">
      <b-card style="max-width: 500px" class="card-header">
        <h2>{{ $t("loginTitle") }}</h2>
        <div class="d-flex justify-content-center" role="alert">
          <label class="text-warning">{{ $t("loginExplanation") }}</label>
        </div>
        <div class="d-flex justify-content-center">
          <span>{{ $t("username") }}</span>
        </div>
        <div class="d-flex justify-content-center mb-3">
          <b-form-input
            v-model="username"
            type="text"
            style="max-width: 150px"
          ></b-form-input>
        </div>
        <div class="d-flex justify-content-center">
          <span> {{ $t("password") }}</span>
        </div>
        <div class="d-flex justify-content-center mb-3">
          <b-form-input
            type="password"
            v-model="password"
            style="max-width: 150px"
          ></b-form-input>
        </div>

        <b-button variant="success" @click="login" v-show="checkFormData">{{
          $t("loginButton")
        }}</b-button>
      </b-card>
    </div>
    <b-button variant="success" @click="logOut" v-show="isUserLogued"
      >Log out</b-button
    >
  </div>
</template>

<script>
import Vue from "vue";
import axios from "axios";

export default {
  name: "Login",
  data() {
    return {
      username: "",
      password: "",
      isUserLogued: false,
    };
  },
  computed: {
    checkFormData() {
      return this.username != "" && this.password != "" ? true : false;
    },
  },
  methods: {
    checkUserLogued() {
      var retrievedObject = localStorage.getItem("token");
      return retrievedObject ? true : false;
    },
    login() {
      const uninterceptedAxiosInstance = axios.create();

      uninterceptedAxiosInstance
        .post(
          this.$config.authenticationServer,
          "grant_type=password&username=" +
            this.username +
            "&password=" +
            this.password,
          {
            headers: { "Content-Type": "text/plain" },
          }
        )
        .then((response) => {
          let expirationDate =
            new Date().getTime() + response.data.expires_in * 1000;

          localStorage.setItem(
            "token",
            JSON.stringify({
              access_token: response.data.access_token,
              expirationDate: expirationDate,
              username: response.data.username,
            })
          );
          
          //go to default page
          this.$router.push("/");
        })
        .catch(() => {
          Vue.notify({
            title: this.$t("notificationError"),
            text: this.$t("notificationLoginError"),
            type: "error",
          });
        });
    },
    logOut() {
      localStorage.removeItem("token");
      this.isUserLogued = false;
    },
  },
  mounted() {
    let ref = this;

    ref.isUserLogued = ref.checkUserLogued();
  },
};
</script>
