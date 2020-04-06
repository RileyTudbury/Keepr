import Vue from "vue";
import Router from "vue-router";
// @ts-ignore
import Home from "./views/Home.vue";
// @ts-ignore
import Dashboard from "./views/Dashboard.vue";
// @ts-ignore
import Vault from "./views/VaultView.vue";
import { authGuard } from "@bcwdev/auth0-vue";

Vue.use(Router);

export default new Router({
  routes: [
    {
      path: "/",
      name: "home",
      component: Home
    },
    {
      path: "/dashboard",
      name: "dashboard",
      component: Dashboard,
      beforeEnter: authGuard
    },
    {
      path: "/dashboard/keeps",
      name: "Dashboard keeps",
      component: Dashboard,
      beforeEnter: authGuard
    },
    {
      path: "/dashboard/vaults",
      name: "Dashboard vaults",
      component: Dashboard,
      beforeEnter: authGuard
    },
    {
      path: "/dashboard/vaults/:vaultId",
      name: 'vault view',
      component: Vault,
      beforeEnter: authGuard

    }

  ]
});
