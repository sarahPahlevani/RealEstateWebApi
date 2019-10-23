<template>
  <div class="row">
    <div class="col-md-12">
      <div class="table-responsive">
        <table class="table table-bordered border-top mb-0">
          <tbody>
            <tr>
              <td>Address1</td>
              <td>
                <span class="font-weight-bold">{{
                  PropertyLocation.addressLine1
                }}</span>
              </td>
            </tr>
            <tr>
              <td>Address2</td>
              <td>
                <span class="font-weight-bold">{{
                  PropertyLocation.addressLine2
                }}</span>
              </td>
            </tr>
            <tr>
              <td>City</td>
              <td>
                <span class="font-weight-bold">1278 fts</span>
              </td>
            </tr>
            <tr>
              <td>ZipCode</td>
              <td>
                <span class="font-weight-bold">{{
                  PropertyLocation.zipCode
                }}</span>
              </td>
            </tr>
          </tbody>
        </table>
      </div>
    </div>
  </div>
</template>
<script>
export default {
  name: 'PropertyLocation',
  data() {
    return {
      PropertyLocation: {}
    }
  },
  created() {
    this.PropertyLocation = this.$store.getters.propertyDetail.propertyLocation
  }
}
</script>
<style></style>
