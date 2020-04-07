<template>
  <div>
    <div class="col-12 text-center mt-2">
      <div class="text-light my-3">
        <h1>{{activeVault.name}}</h1>
        <h3>{{activeVault.description}}</h3>
      </div>
      <div v-if="vaultKeeps.length > 0" class="row">
        <keep
          v-for="(keep, key, index) in vaultKeeps"
          :key="keep.vaultKeepId"
          :index="index"
          :keepData="keep"
          :canKeep="false"
        />
      </div>
      <div v-else class="row">
        <div class="col-12 text-center text-light mt-5">
          <h3>Your Kept keeps will appear here!</h3>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import Keep from "../components/KeepComponent";
import { onAuth } from "@bcwdev/auth0-vue";
export default {
  name: "Vault",
  async beforeCreate() {
    await onAuth();
    this.$store.dispatch("getUserVaults");
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