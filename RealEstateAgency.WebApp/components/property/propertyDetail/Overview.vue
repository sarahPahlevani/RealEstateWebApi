<template>
  <div>
    <h2 class="card-title mb-3 font-weight-semibold">
      Title
    </h2>
    <div class="mb-4">
      <p v-html="propertyDetail.title"></p>
    </div>
    <h3 class="card-title mb-3 font-weight-semibold">
      Description
    </h3>
    <div class="mb-4">
      <p v-html="propertyDetail.description"></p>
    </div>
    <div class="row">
      <div class="col-xl-12 col-md-12">
        <div class="table-responsive">
          <table class="table table-bordered border-top mb-0">
            <tbody>
              <tr>
                <td>Type</td>
                <td>
                  <span class="font-weight-bold">{{
                    propertyDetail.propertyType
                  }}</span>
                </td>
              </tr>

              <tr>
                <td>Label</td>
                <td>
                  <span class="font-weight-bold">{{
                    propertyDetail.propertyLabel
                  }}</span>
                </td>
              </tr>
              <tr>
                <td>Status</td>
                <td>
                  <span class="font-weight-bold">{{
                    propertyDetail.propertyStatus
                  }}</span>
                </td>
              </tr>
              <tr>
                <td>Price</td>
                <td>
                  <span class="font-weight-bold"
                    >${{ propertyDetail.price.price }}</span
                  >
                </td>
              </tr>
            </tbody>
          </table>
        </div>
      </div>
    </div>
  </div>
</template>
<script>
export default {
  name: 'Overview',
  data() {
    return {
      propertyDetail: []
    }
  },
  created() {
    this.propertyDetail = this.$store.getters.propertyDetail
    debugger
  }
}
</script>
<style></style>
