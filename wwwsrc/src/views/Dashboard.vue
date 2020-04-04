<template>
  <div class="dashboard">
    <div class="text-center bg-secondary">
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
    <div v-if="$route.name == 'Dashboard'">
      <h1>WELCOME TO YOUR DASHBOARD</h1>
    </div>
    <div v-else-if="$route.name == 'Dashboard keeps'">KEEPS GO HERE</div>
    <div v-else-if="$route.name == 'Dashboard vaults'">VAULTS GO HERE</div>
  </div>
</template>

<script>
export default {
  mounted() {
    this.$store.dispatch("getPublicKeeps");
  },
  computed: {
    publicKeeps() {
      return this.$store.state.publicKeeps;
    },
    userKeeps() {
      return this.$store.state.userKeeps;
    }
  }
};
</script>

<style></style>
