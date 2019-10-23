<template>
  <div>
    <div class="row">
      <div class="col-xl-12 col-md-12">
        <div class="table-responsive">
          <table class="table table-bordered border-top mb-0">
            <tbody>
              <tr>
                <td>Size</td>
                <td>
                  <span class="font-weight-bold">{{
                    propertyDetail.size
                  }}</span>
                </td>
              </tr>
              <tr>
                <td>Land Area</td>
                <td>
                  <span class="font-weight-bold">{{
                    propertyDetail.landArea
                  }}</span>
                </td>
              </tr>
              <tr v-if="propertyDetail.rooms !== null">
                <td>Rooms</td>
                <td>
                  <span class="font-weight-bold">{{
                    propertyDetail.rooms
                  }}</span>
                </td>
              </tr>
              <tr v-if="propertyDetail.bedrooms !== null">
                <td>BedRooms</td>
                <td>
                  <span class="font-weight-bold">{{
                    propertyDetail.bedrooms
                  }}</span>
                </td>
              </tr>
              <tr v-if="propertyDetail.bathrooms !== null">
                <td>BathRooms</td>
                <td>
                  <span class="font-weight-bold">{{
                    propertyDetail.bathrooms
                  }}</span>
                </td>
              </tr>
              <tr v-if="propertyDetail.garages !== null">
                <td>Car Parking</td>
                <td>
                  <span class="font-weight-bold"
                    >2{{ propertyDetail.garages }}</span
                  >
                </td>
              </tr>
              <tr v-if="propertyDetail.garageSize !== null">
                <td>Car Parking Size</td>
                <td>
                  <span class="font-weight-bold"
                    >2{{ propertyDetail.garageSize }}</span
                  >
                </td>
              </tr>
              <tr v-if="propertyDetail.yearBuild !== null">
                <td>Build Year</td>
                <td>
                  <span class="font-weight-bold">{{
                    propertyDetail.yearBuild
                  }}</span>
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
    this.propertyDetail = this.$store.getters.propertyDetail.propertyDetail
  }
}
</script>
<style></style>
