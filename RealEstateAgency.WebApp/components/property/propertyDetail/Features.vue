<template>
  <div class="row" v-if="features.length > 0">
    <div v-for="(feature, index) in features" :key="index" class="col-md-3">
      <label class="mb-3">
        <i class="fa fa-check-square-o icon-color"></i>
        <span>
          <span class="text-dark"> {{ feature }}</span>
        </span>
      </label>
      <!--    <label class="custom-control custom-checkbox mb-3">-->
      <!--      <input-->
      <!--        type="checkbox"-->
      <!--        class="custom-control-input"-->
      <!--        name="checkbox2"-->
      <!--        value="option2"-->
      <!--      />-->
      <!--      <span class="custom-control-label">-->
      <!--        <span class="text-dark"-->
      <!--          >Apartmenrs<span class="label label-secondary float-right"-->
      <!--            >22</span-->
      <!--          ></span-->
      <!--        >-->
      <!--      </span>-->
      <!--    </label>-->
    </div>
  </div>
  <div class="row" v-else>
    <h2 style="text-align: center; color: #24abe3;">No features added</h2>
  </div>
</template>
<script>
export default {
  name: 'PropertyFeatures',
  propertyFeatures: [],
  data() {
    return {
      propertyFeature: [],
      features: []
    }
  },
  created() {
    this.propertyFeature = this.$store.getters.propertyDetail.featureIds

    this.propertyFeature.forEach((el, index) => {
      const item = this.$store.getters.propertyFeatures.filter(
        (i) => i.id === el
      )
      this.features.push(item[0].name)
    })
  }
}
</script>
<style>
.icon-color {
  color: #24abe3;
}
</style>
