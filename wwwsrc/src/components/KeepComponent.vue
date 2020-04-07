<template>
  <div class="col-12 col-md-3 pb-2">
    <div class="card">
      <div v-if="canKeep" class="card-header">
        <form @submit.prevent="createVaultKeep(keepData)" class="form-inline" action>
          <div class="form-group">
            <label>Keep it:</label>
            <select class="mx-2 form-control-sm" v-model="newVaultKeep.vaultId">
              <option v-for="vault in userVaults" :key="vault.id" :value="vault.id">{{vault.name}}</option>
            </select>
          </div>
          <button type="submit" class="btn btn-sm btn-success">+</button>
        </form>
      </div>
      <div v-else-if="$route.name == 'vault view'" class="card-header">
        Remove from Vault
        <button
          @click="removeFromVault(keepData.vaultKeepId)"
          class="btn btn-danger btn-sm"
        >X</button>
      </div>
      <img :src="keepData.img" class="card-img-top" />
      <p class="card-header">{{keepData.name}}</p>
      <div class="card-body">
        <p class="card-text">{{keepData.description}}</p>
        <span>
          Shares: {{keepData.shares}}
          Keeps: {{keepData.keeps}}
          Views: {{keepData.views}}
        </span>
        <div v-if="(keepData.userId == this.$auth.userInfo.sub) && keepData.isPrivate">
          <button @click="deleteKeep(keepData.id)" class="btn btn-sm btn-danger mt-1">Delete Keep</button>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: "Keep",
  props: ["canKeep", "keepData", "index"],
  data() {
    return {
      newVaultKeep: {
        keepId: "",
        vaultId: ""
      }
    };
  },
  mounted() {},
  computed: {
    userVaults() {
      return this.$store.state.userVaults;
    }
  },
  methods: {
    deleteKeep(keepId) {
      this.$store.dispatch("deleteKeep", keepId);
    },
    createVaultKeep(keepData) {
      this.newVaultKeep.keepId = keepData.id;
      this.$store.dispatch("createVaultKeep", this.newVaultKeep);
      keepData.keeps++;
      this.$store.dispatch("updateKeepCounts", keepData);
    },
    removeFromVault(vaultKeepId) {
      this.$store.dispatch("deleteVaultKeep", vaultKeepId);
    }
  }
};
</script>

<style>
select {
  width: auto;
}
</style>