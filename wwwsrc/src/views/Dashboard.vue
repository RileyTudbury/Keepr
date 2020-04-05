<template>
  <div class="dashboard">
    <div class="text-center bg-secondary mb-2">
      <ul class="nav nav-tabs">
        <li class="nav-item">
          <router-link
            :to="{ name: 'Dashboard keeps' }"
            :class="{active:$route.name == 'Dashboard keeps'}"
            class="nav-link"
          >My Keeps</router-link>
        </li>
        <li>
          <router-link
            :to="{ name: 'Dashboard vaults' }"
            :class="{active:$route.name == 'Dashboard vaults'}"
            class="nav-link"
          >My Vaults</router-link>
        </li>
      </ul>
    </div>
    <div v-if="$route.name == 'dashboard'">
      <h1>WELCOME TO YOUR DASHBOARD</h1>
    </div>
    <div class="container-fluid" v-else-if="$route.name == 'Dashboard keeps'">
      <div class="row">
        <div class="col-12">
          <button class="btn btn-block btn-info mb-2">Create New Keep</button>
        </div>
        <keep
          v-for="(keep, key, index) in userKeeps"
          :key="keep.id"
          :index="index"
          :keepData="keep"
        />
      </div>
    </div>
    <div v-else-if="$route.name == 'Dashboard vaults'">VAULTS GO HERE</div>
  </div>
</template>

<script>
import { onAuth } from "@bcwdev/auth0-vue";
import Keep from "../components/KeepComponent";
export default {
  name: "dashboard",
  async beforeCreate() {
    this.$store.dispatch("getPublicKeeps");
    await onAuth();
    this.$store.dispatch("setBearer", this.$auth.bearer);
    this.$store.dispatch("getUserKeeps");
  },
  mounted() {},
  computed: {
    publicKeeps() {
      return this.$store.state.publicKeeps;
    },
    userKeeps() {
      return this.$store.state.userKeeps;
    }
  },
  components: {
    Keep
  }
};
</script>

<style></style>
