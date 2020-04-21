<template>
  <div class="col-12 col-md-3 pb-2">
    <div @mouseenter="viewKeep(keepData)" class="keep-container row">
      <img :src="keepData.img" class="image" />
      <div class="middle text-light text-center col-12">
        <h4>{{keepData.name}}</h4>
        <p>{{keepData.description}}</p>
        <button
          class="btn btn-danger btn-sm"
          v-if="$route.name == 'vault view'"
          @click="removeFromVault(keepData.vaultKeepId)"
        >Remove from Vault</button>
        <div class="bottom row pb-3">
          <div class="col-12 mb-1">
            <form @submit.prevent="createVaultKeep(keepData)" class="ml-4 mb-1 form-inline" action>
              <div class="form-group">
                <label>Keep it:</label>
                <select class="mx-2 form-control-sm" v-model="newVaultKeep.vaultId">
                  <option
                    v-for="vault in userVaults"
                    :key="vault.id"
                    :value="vault.id"
                  >{{vault.name}}</option>
                </select>
                <button type="submit" class="btn btn-sm btn-success px-1 py-0">+</button>
              </div>
            </form>
          </div>
          <span class="col-4">Keeps: {{keepData.keeps}}</span>
          <span class="col-4">
            <i class="fas fa-share-alt-square"></i>
            : {{keepData.shares}}
          </span>
          <span class="col-4">
            <i class="far fa-eye"></i>
            : {{keepData.views}}
          </span>
        </div>
      </div>
    </div>
    <!-- 
     <div class="card">
      <div v-if="(userVaults.length > 0) && canKeep" class="card-header">
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
      <img @click="viewKeep(keepData)" :src="keepData.img" class="card-img-top" />
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
    </div>-->
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
    viewKeep(keepData) {
      keepData.views++;
      this.$store.dispatch("updateKeepCounts", keepData);
    },
    removeFromVault(vaultKeepId) {
      this.$store.dispatch("deleteVaultKeep", vaultKeepId);
    }
  }
};
</script>

<style scoped>
select {
  width: auto;
}

.text-light {
  text-shadow: 1px 1px 2px black;
}

/* Hover On Keeps */

.keep-container {
  position: relative;
  width: 100%;
  height: auto;
}

.image {
  opacity: 1;
  display: block;
  width: 100%;
  height: auto;
  transition: 0.5s ease;
  backface-visibility: hidden;
}

.middle {
  transition: 0.5s ease;
  opacity: 0;
  position: absolute;
  top: 50%;
  left: 50%;
  transform: translate(-50%, -50%) scale(0.9, 0.9);
  -ms-transform: translate(-50%, -50%);
  text-align: center;
  height: 100%;
  width: 100%;
}

.bottom {
  width: 100%;
  position: absolute;
  bottom: 0;
}

.keep-container:hover .image {
  opacity: 0.5;
  transform: scale(1.02, 1.02);
}

.keep-container:hover .middle {
  opacity: 1;
  transform: translate(-50%, -50%) scale(1, 1);
}
</style>