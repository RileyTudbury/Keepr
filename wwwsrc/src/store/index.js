import Vue from "vue";
import Vuex from "vuex";
import Axios from "axios";
import router from "../router";

Vue.use(Vuex);

let baseUrl = location.host.includes("localhost")
  ? "https://localhost:5001/"
  : "/";

let api = Axios.create({
  baseURL: baseUrl + "api/",
  timeout: 3000,
  withCredentials: true
});

export default new Vuex.Store({
  state: {
    publicKeeps: [],
    userKeeps: [],
    vaultKeeps: [],
    userVaults: []
  },
  mutations: {
    //Keep Mutations
    setPublicKeeps(state, keeps) {
      state.publicKeeps = keeps;
    },
    setUserKeeps(state, userKeeps) {
      state.userKeeps = userKeeps;
    },
    addKeep(state, keep) {
      state.userKeeps.push(keep)
      if (!keep.isPrivate) {
        state.publicKeeps.push(keep)
      }
    },
    removeKeep(state, keepId) {
      state.publicKeeps = state.publicKeeps.filter(k => k.id != keepId);
      state.userKeeps = state.userKeeps.filter(k => k.id != keepId);
    },
    //Vault Mutations
    setUserVaults(state, userVaults) {
      state.userVaults = userVaults
    },
    addVault(state, vault) {
      state.userVaults.push(vault)
    },
    removeVault(state, vaultId) {
      state.userVaults = state.userVaults.filter(v => v.id != vaultId)
    },
    //VaultKeep Mutations
    setVaultKeeps(state, vaultKeeps) {
      state.vaultKeeps = vaultKeeps
    },
    addVaultKeep(state, vaultKeep) {
      state.vaultKeeps.push(vaultKeep)
    }
  },
  actions: {
    setBearer({ }, bearer) {
      api.defaults.headers.authorization = bearer;
    },
    resetBearer() {
      api.defaults.headers.authorization = "";
    },

    //Keep Actions
    async getPublicKeeps({ commit }) {
      let res = await api.get("keeps");
      commit("setPublicKeeps", res.data);
    },
    async getUserKeeps({ commit }) {
      let res = await api.get("keeps/mykeeps");
      commit("setUserKeeps", res.data);
    },
    async createKeep({ commit }, keepData) {
      let res = await api.post("keeps", keepData)
      commit("addKeep", res.data)
    },
    async deleteKeep({ commit }, keepId) {
      let res = await api.delete(`keeps/${keepId}`)
      commit("removeKeep", keepId)
    },

    //Vault Actions
    async getUserVaults({ commit }) {
      let res = await api.get("vaults");
      commit("setUserVaults", res.data)
    },
    async createVault({ commit }, vaultData) {
      let res = await api.post("vaults", vaultData)
      commit("addVault", res.data)
    },
    async deleteVault({ commit }, vaultId) {
      let res = await api.delete(`vaults/${vaultId}`)
      commit("removeVault", vaultId)
    },

    //VaultKeep Actions
    async createVaultKeep({ commit }, vaultKeep) {
      debugger
      let res = await api.post("vaultkeeps", vaultKeep)
      commit("addVaultKeep", res.data)
    }

  }
});
