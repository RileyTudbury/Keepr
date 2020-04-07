<template>
  <div class="home">
    <div class="container-fluid">
      <div class="row">
        <div class="col-12 text-center text-light py-4">
          <h1>Welcome To Keepr!</h1>
        </div>
      </div>
      <div v-if="this.$auth.isAuthenticated" class="row">
        <keep v-for="keep in keeps" :key="keep.id" :keepData="keep" :canKeep="true" />
      </div>
      <div v-else class="row">
        <keep v-for="keep in keeps" :key="keep.id" :keepData="keep" :canKeep="false" />
      </div>
    </div>
  </div>
</template>

<script>
import Keep from "../components/KeepComponent";
import { onAuth } from "@bcwdev/auth0-vue";
export default {
  name: "home",
  data() {
    return {};
  },
  components: {
    Keep
  },
  computed: {
    user() {
      return this.$store.state.user;
    },
    keeps() {
      return this.$store.state.publicKeeps;
    }
  },
  methods: {
    logout() {
      this.$store.dispatch("logout");
    }
  }
};
</script>
