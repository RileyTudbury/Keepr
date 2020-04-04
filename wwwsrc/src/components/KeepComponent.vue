<template>
  <div class="col-12 col-md-3 pb-2">
    <div class="card">
      <img :src="keepData.img" class="card-img-top" />
      <p class="card-header">{{keepData.name}}</p>
      <div class="card-body">
        <p class="card-text">{{keepData.description}}</p>
        <span>
          Shares: {{keepData.shares}}
          Keeps: {{keepData.keeps}}
          Views: {{keepData.views}}
        </span>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: "Keep",
  props: ["keepData", "index"]
};
</script>

<style>
</style>