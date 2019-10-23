<template>
  <div class="card mb-0">
    <div class="card-header">
      <h3 class="card-title">My Ads</h3>
    </div>
    <div class="card-body">
      <div class="my-favadd table-responsive border-top userprof-tab">
        <table class="table table-bordered table-hover mb-0 text-nowrap">
          <thead>
            <tr>
              <th></th>
              <th>Item</th>
              <th>Category</th>
              <th>Price</th>
              <th>Ad Status</th>
              <th>Action</th>
            </tr>
          </thead>
          <tbody>
            <ThumbnailProperty />
            <ThumbnailProperty />
            <ThumbnailProperty />
            <ThumbnailProperty />
          </tbody>
        </table>
      </div>
    </div>
  </div>
</template>

<script>
import ThumbnailProperty from '../../components/mainComponents/ThumbnailProperty'
export default {
  name: 'MyAds',
  components: {
    ThumbnailProperty
  }
}
</script>

<style scoped></style>
