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
          <!-- Button trigger modal -->
          <button
            type="button"
            data-toggle="modal"
            data-target="#keep-form"
            class="btn btn-block btn-info mb-2"
          >Create New Keep</button>
          <!-- Modal -->
          <div class="modal fade" id="keep-form" tabindex="-1" role="dialog">
            <div class="modal-dialog modal-dialog-centered" role="document">
              <div class="modal-content">
                <div class="modal-header">
                  <h5 class="modal-title" id="exampleModalLongTitle">Create a new Keep</h5>
                  <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                    <span aria-hidden="true">&times;</span>
                  </button>
                </div>
                <div class="modal-body">
                  <form @submit.prevent="createKeep">
                    <div class="form-group">
                      <label for="nameInput">Keep Name</label>
                      <input
                        v-model="newKeep.name"
                        type="text"
                        class="form-control"
                        id="nameInput"
                        placeholder="Enter Keep Name.."
                        required
                      />
                    </div>
                    <div class="form-group">
                      <label for="descInput">Keep Description</label>
                      <input
                        v-model="newKeep.description"
                        type="text"
                        class="form-control"
                        id="descInput"
                        placeholder="Description.."
                        required
                      />
                    </div>
                    <div class="form-group">
                      <label for="imgInput">Keep Img Url</label>
                      <input
                        value="http://placehold.it/150"
                        v-model="newKeep.img"
                        type="text"
                        class="form-control"
                        id="imgInput"
                        placeholder="Img URL.."
                        required
                      />
                    </div>
                    <div class="form-check">
                      <label class="form-check-label" for="privateCheck">
                        <input
                          @click="newKeep.isPrivate = !newKeep.isPrivate"
                          type="checkbox"
                          class="form-check-input"
                          id="privateCheck"
                        />Keep this private?
                      </label>
                    </div>
                    <small>Private Keeps will not be displayed on the homepage</small>
                    <div class="modal-footer">
                      <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                      <button type="submit" class="btn btn-primary">Keep It!</button>
                    </div>
                  </form>
                </div>
              </div>
            </div>
          </div>
        </div>
        <keep
          v-for="(keep, key, index) in userKeeps"
          :key="keep.id"
          :index="index"
          :keepData="keep"
        />
      </div>
    </div>
    <div class="container-fluid" v-else-if="$route.name == 'Dashboard vaults'">
      <div class="row">
        <div class="col-12">
          <!-- Button trigger modal -->
          <button
            type="button"
            data-toggle="modal"
            data-target="#vault-form"
            class="btn btn-block btn-info mb-2"
          >Create New Vault</button>
          <!-- Modal -->
          <div class="modal fade" id="vault-form" tabindex="-1" role="dialog">
            <div class="modal-dialog modal-dialog-centered" role="document">
              <div class="modal-content">
                <div class="modal-header">
                  <h5 class="modal-title" id="exampleModalLongTitle">Create a new Vault</h5>
                  <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                    <span aria-hidden="true">&times;</span>
                  </button>
                </div>
                <div class="modal-body">
                  <form @submit.prevent="createVault">
                    <div class="form-group">
                      <label for="vaultNameInput">Vault Name</label>
                      <input
                        v-model="newVault.name"
                        type="text"
                        class="form-control"
                        id="vaultNameInput"
                        placeholder="Enter Vault Name.."
                        required
                      />
                    </div>
                    <div class="form-group">
                      <label for="vaultDescInput">Vault Description</label>
                      <input
                        v-model="newVault.description"
                        type="text"
                        class="form-control"
                        id="vaultDescInput"
                        placeholder="Description.."
                        required
                      />
                    </div>
                    <div class="modal-footer">
                      <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                      <button type="submit" class="btn btn-primary">Vault Em!</button>
                    </div>
                  </form>
                </div>
              </div>
            </div>
          </div>
        </div>
        <div class="col-3" v-for="vault in userVaults" :key="vault.id" :vaultData="vault">
          <div class="card">
            <router-link :to="{name: 'vault view', params: {vaultId: vault.id}}">
              <img
                class="card-img-top"
                src="https://www.gleason-group.net/wp-content/uploads/2018/03/vault-1.jpg"
              />
            </router-link>
            <div class="card-body">
              <p class="card-text">{{vault.name}}</p>
              <p class="card-text">{{vault.description}}</p>
              <button @click="deleteVault(vault.id)" class="btn btn-sm btn-danger">Delete</button>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { onAuth } from "@bcwdev/auth0-vue";
import Keep from "../components/KeepComponent";
import Vault from "../components/VaultComponent";
export default {
  name: "dashboard",
  async beforeCreate() {
    this.$store.dispatch("getPublicKeeps");
    await onAuth();
    this.$store.dispatch("setBearer", this.$auth.bearer);
    this.$store.dispatch("getUserKeeps");
    this.$store.dispatch("getUserVaults");
  },
  data() {
    return {
      newKeep: {
        name: "",
        description: "",
        img: "http://placehold.it/150",
        isPrivate: false
      },
      newVault: {
        name: "",
        description: ""
      }
    };
  },
  mounted() {},
  methods: {
    createKeep() {
      this.$store.dispatch("createKeep", this.newKeep);
      this.newKeep = {
        name: "",
        description: "",
        img: "http://placehold.it/150",
        isPrivate: false
      };
    },
    createVault() {
      this.$store.dispatch("createVault", this.newVault);
      this.newVault = {
        name: "",
        description: ""
      };
      console.log(this.$store.state.userVaults);
    },
    deleteVault(vaultId) {
      this.$store.dispatch("deleteVault", vaultId);
    }
  },
  computed: {
    publicKeeps() {
      return this.$store.state.publicKeeps;
    },
    userKeeps() {
      return this.$store.state.userKeeps;
    },
    userVaults() {
      return this.$store.state.userVaults;
    }
  },
  components: {
    Keep
  }
};
</script>

<style>
</style>
