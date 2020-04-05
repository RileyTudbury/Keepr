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
                        type="text"
                        class="form-control"
                        id="nameInput"
                        placeholder="Enter Keep Name.."
                      />
                    </div>
                    <div class="form-group">
                      <label for="descInput">Keep Description</label>
                      <input
                        type="text"
                        class="form-control"
                        id="descInput"
                        placeholder="Description.."
                      />
                    </div>
                    <div class="form-group">
                      <label for="imgInput">Keep Img Url</label>
                      <input type="text" class="form-control" id="imgInput" placeholder="Img URL.." />
                    </div>
                    <div class="form-check">
                      <input type="checkbox" class="form-check-input" id="privateCheck" />
                      <label class="form-check-label" for="privateCheck">Keep this private?</label>
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
    <div v-else-if="$route.name == 'Dashboard vaults'">VAULTS GO HERE</div>
  </div>
</template>

<script>
import { onAuth } from "@bcwdev/auth0-vue";
import Keep from "../components/KeepComponent";
export default {
  name: "dashboard",
  async beforeCreate() {
    this.$store.dispatch("getPublicKeeps");
    await onAuth();
    this.$store.dispatch("setBearer", this.$auth.bearer);
    this.$store.dispatch("getUserKeeps");
  },
  mounted() {},
  methods: {
    //TODO Make createKeep do something
    createKeep() {
      console.log("Submitted!");
    }
  },
  computed: {
    publicKeeps() {
      return this.$store.state.publicKeeps;
    },
    userKeeps() {
      return this.$store.state.userKeeps;
    }
  },
  components: {
    Keep
  }
};
</script>

<style>
</style>
