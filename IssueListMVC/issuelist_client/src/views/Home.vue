<template>
  <div class="home">
    <div>
      <typical
        :steps="[$t('AppTitle1'), 2000, $t('AppTitle2'), 2500]"
        :wrapper="'h2'"
      ></typical>
      <typical
        class="typicalWrapper"
        :steps="[
          $t('AppSubTitle1'),
          1000,
          $t('AppSubTitle2'),
          1500,
          $t('AppSubTitle3'),
          2500,
        ]"
        :wrapper="'h3'"
      ></typical>
      <div class="d-flex justify-content-center pt-2">
        <label class="pt-2">{{ $t("filter") }} </label>
        <b-form-input
          id="inputTitle"
          v-model="filterTitle"
          style="max-width: 150px"
        ></b-form-input>
      </div>
      <div class="d-flex justify-content-end">
        <b-button
          variant="success"
          @click="createIssue"
          style="margin-right: 15px"
          >{{ $t("buttonIssueAdd") }}
          </b-button>
      </div>
    </div>
    <div v-for="item in filterIssueByTitle" :key="item.id" class="pt-3">
      <IssueItem
        :severityList="severityList"
        :statusList="statusList"
        :issue="item"
        :users="users"
      ></IssueItem>
    </div>
  </div>
</template>

<script>
import axios from "axios";
import IssueItem from "../components/IssueItem.vue";
import typical from "vue-typical";
import Vue from "vue";

export default {
  name: "Home",
  components: {
    IssueItem,
    typical,
  },
  props: {
    msg: String,
  },
  data() {
    return {
      issueList: [],
      users: [],
      severityList: [],
      statusList: [],
      filterTitle: "",
    };
  },
  computed: {
    filterIssueByTitle() {
      if (this.filterTitle.length > 0) {
        var aux = this.issueList.filter((item) =>
          item.title
            ? item.title.toUpperCase().includes(this.filterTitle.toUpperCase())
            : false
        );
        return aux;
      } else return this.issueList;
    },
  },
  methods: {
    getUsers() {
      axios.get(this.$config.apiUsers).then((respuesta) => {
        this.users = respuesta.data.map((val) => ({
          value: val.id,
          text: val.username,
        }));
      });
    },
    getSeverityEnum() {
      axios
        .get(this.$config.apiServer + "/Enum/GetSeverityEnum")
        .then((respuesta) => {
          this.severityList = respuesta.data;
        });
    },
    getStatusList() {
      axios
        .get(this.$config.apiServer + "/Enum/GetStatusEnum")
        .then((respuesta) => {
          this.statusList = respuesta.data;
        });
    },
    createIssue() {
      let newIssue = {
        id: 0,
        title: "",
        description: "",
        severity: 1,
        status: 1,
        asignee: null,
      };
      axios.post(this.$config.apiServer + "/Issue", newIssue).then(() => {
        Vue.notify({
          title: this.$t("notificationIssueCreatedTitle"),
          text: this.$t("notificationIssueCreated"),
          type: "success",
        });

        //scroll to page end
        window.scrollTo(
          0,
          document.body.scrollHeight || document.documentElement.scrollHeight
        );
      });
    },
    getUserClaims() {
      axios.get(this.$config.apiServer + "/Claims").then((response) => {
        let userClaim = response.data.filter(
          (claim) => claim.m_type == "user"
        )[0];
        this.$parent.username = userClaim.m_value;
      });
    },
    updateSocketMessagge(event) {
      this.issueList = JSON.parse(event.data);
    },
  },
  mounted() {
    var ref = this;

    //get data
    this.getUsers();
    this.getSeverityEnum();
    this.getStatusList();
    this.getUserClaims();

    //start WebSocket
    let serverAdress = this.$config.wsServer;
    this.connection = new WebSocket(serverAdress);

    this.connection.onmessage = function (event) {
      ref.updateSocketMessagge(event);
    };

    this.connection.onopen = function () {};

    this.connection.onclose = function () {
      //reconect socket when closed
      setTimeout(function () {
        this.connection = new WebSocket(serverAdress);
      }, 30000);
    };
  },
};
</script>
