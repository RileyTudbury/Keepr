<template>
  <div>
    <div class="col-12 text-center mt-2">
      <h1>{{activeVault.name}}</h1>
      <h3>{{activeVault.description}}</h3>
      <div class="row">
        <keep
          v-for="(keep, key, index) in vaultKeeps"
          :key="keep.id"
          :index="index"
          :keepData="keep"
        />
      </div>
    </div>
  </div>
</template>

<script>
import Keep from "../components/KeepComponent";
export default {
  name: "Vault",
  mounted() {
    this.$store.dispatch("setActiveVault", this.$route.params.vaultId);
    this.$store.dispatch("getKeepsByVaultId", this.$route.params.vaultId);
  },
  computed: {
    vaultKeeps() {
      return this.$store.state.vaultKeeps;
    },
    activeVault() {
      return this.$store.state.activeVault;
    }
  },
  components: {
    Keep
  }
};
</script>

<style>
</style>