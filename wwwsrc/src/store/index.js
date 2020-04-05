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
    userKeeps: []
  },
  mutations: {
    setPublicKeeps(state, keeps) {
      state.publicKeeps = keeps;
    },
    SetUserKeeps(state, userKeeps) {
      state.userKeeps = userKeeps;
    },
    addKeep(state, keep) {
      state.userKeeps.push(keep)
      if (!keep.isPrivate) {
        state.publicKeeps.push(keep)
      }
    }
  },
  actions: {
    setBearer({ }, bearer) {
      api.defaults.headers.authorization = bearer;
    },
    resetBearer() {
      api.defaults.headers.authorization = "";
    },

    async getPublicKeeps({ commit }) {
      let res = await api.get("keeps");
      commit("setPublicKeeps", res.data);
    },
    async getUserKeeps({ commit }) {
      let res = await api.get("keeps/mykeeps");
      commit("SetUserKeeps", res.data);
    },
    async createKeep({ commit }, keepData) {
      let res = await api.post("keeps", keepData)
      commit("addKeep", res.data)
    }

  }
});
