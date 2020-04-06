<template>
  <div>
    <div class="col-12 text-center mt-2">
      <h1>{{activeVault.name}}</h1>
      <h3>{{activeVault.description}}</h3>
    </div>
  </div>
</template>

<script>
export default {
  name: "Vault",
  mounted() {
    this.$store.dispatch("setActiveVault", this.$route.params.vaultId);
  },
  computed: {
    vaultKeeps() {
      return this.$store.state.vaultKeeps;
    },
    activeVault() {
      return this.$store.state.activeVault;
    }
  }
};
</script>

<style>
</style>