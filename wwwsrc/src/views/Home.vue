<template>
  <div class="home">
    <div class="container-fluid">
      <div class="row">
        <div class="col-12 text-center pt-2">
          <h1>Welcome To Keepr</h1>
        </div>
      </div>
      <div class="row">
        <keep v-for="(keep, key, index) in keeps" :key="keep.id" :index="index" :keepData="keep" />
      </div>
    </div>
  </div>
</template>

<script>
import Keep from "../components/KeepComponent";
export default {
  name: "home",
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
