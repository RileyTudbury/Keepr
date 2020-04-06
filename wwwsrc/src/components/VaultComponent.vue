<template>
  <div>
    <h1>Name: {{activeVault.name}}</h1>
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