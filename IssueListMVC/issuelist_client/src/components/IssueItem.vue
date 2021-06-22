<template>
  <div>
    <b-card
      no-body
      v-bind:class="changeSeverityColor"
      class="overflow-hidden"
      style="margin-left: 15px; margin-right: 15px">
      <b-row no-gutters>
        <b-col md="12">
          <b-card-body>
            <title></title>
            <b-card-text>
              <div class="d-flex justify-content-end">
                <b-icon
                  class="icon-rigth"
                  icon="trash"
                  @click="deleteIssue"
                ></b-icon>
              </div>
              <b-row>
                <b-col>
                  <b-form-group
                    :label="$t('title')"
                    label-for="inputTitle"                  >
                    <b-form-input
                      :id="'title_' + this.issue.id"
                      v-model="issue.title"
                      @change="onChangeIssue()"
                      trim
                      maxlength="50">
                    ></b-form-input>
                  </b-form-group>
                </b-col>
                <b-col>
                  <b-form-group
                    :label="$t('description')"
                    label-for="inputDescription"               
                  >
                    <b-form-input
                      :id="'description_' + this.issue.id"
                      v-model="issue.description"
                       @change="onChangeIssue()"
                       maxlength="100"
                      trim
                    ></b-form-input>
                  </b-form-group>
                </b-col>
              </b-row>

              <b-container class="bv-example-row">
                <b-row class="pt-4">
                  <b-col>
                    <b-form-group :label="$t('severity')">
                      <b-form-select
                        v-model="issue.severity"
                        :options="severityList"
                        @change="onChangeIssue()"
                      ></b-form-select>
                    </b-form-group>
                  </b-col>
                  <b-col>
                    <b-form-group :label="$t('status')">
                      <b-form-select
                        v-model="issue.status"
                        :options="statusList"
                        @change="onChangeIssue()"
                      ></b-form-select>
                    </b-form-group>
                  </b-col>
                  <b-col>
                    <b-form-group :label="$t('assignee')">
                      <b-form-select
                        v-model="issue.asignee"
                        :options="users"
                        @change="onChangeIssue()"
                        class="mb-3"
                      ></b-form-select>
                    </b-form-group>
                  </b-col>
                </b-row>
              </b-container>
            </b-card-text>
          </b-card-body>
        </b-col>
      </b-row>
    </b-card>
  </div>
</template>

<script>
import Vue from "vue";
import axios from "axios";

export default {
  name: "IssueItem",

  props: {
    severityList: Array,
    statusList: Array,
    issue: Object,
    users: Array,
  },
  computed: {
    changeSeverityColor() {
      switch (this.issue.severity) {
        case 0:
          return "alert-danger";
        case 1:
          return "alert-warning";
        case 2:
          return "alert-success";
        default:
          return "";
      }
    },
  },
  data() {
    return {
      
    };
  },
  methods: {
    onChangeIssue() {
      axios
        .put(this.$config.apiServer + "/Issue/" + this.issue.id, this.issue);
    },
    deleteIssue() {
      axios
        .delete(this.$config.apiServer + "/Issue/" + this.issue.id)
        .then(() => {
          Vue.notify({
            title: this.$t('delete'),
            text: this.$t('deleteIssue'),
            type: "warn",
          });
        });
    },
  }
};
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
.custom-select {
  display: inline-block;
  width: 100%;
  height: calc(1.5em + 0.75rem + 2px);
  padding: 0.375rem 1.75rem 0.375rem 0.75rem;
  font-size: 1rem;
  font-weight: 400;
  line-height: 1.5;
  color: #495057;
  vertical-align: middle;
  background: #fff
    url("data:image/svg+xml;charset=utf-8,%3Csvg xmlns='http://www.w3.org/2000/svg' width='4' height='5'%3E%3Cpath fill='%23343a40' d='M2 0L0 2h4zm0 5L0 3h4z'/%3E%3C/svg%3E")
    no-repeat right 0.75rem center/8px 10px;
  border: 1px solid #ced4da;
  border-radius: 0.25rem;
  -webkit-appearance: none;
  -moz-appearance: none;
  appearance: none;
}
</style>
