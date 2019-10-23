<template>
  <div class="item">
    <div class="card mb-0">
      <div class="power-ribbon power-ribbon-top-left text-warning">
        <span class="bg-warning"><i class="fa fa-bolt"></i></span>
      </div>
      <div class="item-card2-img">
        <nuxt-link :to="`propertyDetails/${property.id}`"></nuxt-link>
        <img :src="property.imagesUrl" alt="img" class="cover-image" />
        <div class="tag-text">
          <span class="bg-danger tag-option">{{ property.status }} </span>
          <span class="bg-pink tag-option">{{ property.type }}</span>
        </div>
      </div>
      <div class="item-card2-icons">
        <nuxt-link :to="`propertyDetails/${property.id}`"
          ><a class="item-card2-icons-l bg-primary">
            <i class="fa fa-home"></i></a
        ></nuxt-link>
        <!--        <nuxt-link :to="`propertyDetails/${property.id}`"-->
        <!--          ><a class="item-card2-icons-r bg-secondary"-->
        <!--            ><i class="fa fa fa-heart-o"></i></a-->
        <!--        ></nuxt-link>-->
      </div>
      <div class="card-body">
        <div class="item-card2">
          <div class="item-card2-text">
            <nuxt-link :to="`propertyDetails/${property.id}`"
              ><a class="text-dark"
                ><h4 class="">{{ property.title }}</h4></a
              ></nuxt-link
            >
            <p class="mb-2">
              <i class="fa fa-map-marker text-danger mr-1"></i>
              {{ property.propertyLocation.addressLine1 }}
              Wichita , USA
            </p>
            <h5 class="font-weight-bold mb-3">
              ${{ property.price.price }}
              <span class="fs-12  font-weight-normal">Per Month</span>
            </h5>
          </div>
          <ul class="item-card2-list">
            <li class="prop-card-icon">
              <nuxt-link :to="`propertyDetails/${property.id}`">
                <a
                  ><i class="fa fa-arrows-alt text-muted mr-1"></i>
                  <span v-if="property.propertyDetail.size !== null"
                    >{{ property.propertyDetail.size }} </span
                  ><span v-else>0</span> Sqft</a
                >
              </nuxt-link>
            </li>
            <li class="prop-card-icon">
              <nuxt-link :to="`propertyDetails/${property.id}`"
                ><a class="icons"
                  ><i class="fa fa-bed text-muted mr-1"></i>
                  <span v-if="property.propertyDetail.bedrooms !== null">{{
                    property.propertyDetail.bedrooms
                  }}</span
                  ><span v-else>0</span> Beds</a
                ></nuxt-link
              >
            </li>
            <li class="prop-card-icon">
              <nuxt-link :to="`propertyDetails/${property.id}`">
                <a class="icons"
                  ><i class="fa fa-bath text-muted mr-1"></i>
                  <span v-if="property.propertyDetail.bathrooms !== null"
                    >{{ property.propertyDetail.bathrooms }} </span
                  ><span v-else>0</span>
                  Bath
                </a>
              </nuxt-link>
            </li>
            <li class="prop-card-icon">
              <nuxt-link :to="`propertyDetails/${property.id}`">
                <a class="icons"
                  ><i class="fa fa-car text-muted mr-1"></i>
                  <span v-if="property.propertyDetail.garages !== null">{{
                    property.propertyDetail.garages
                  }}</span
                  ><span v-else>0</span> Car</a
                >
              </nuxt-link>
            </li>
          </ul>
        </div>
      </div>
      <div class="card-footer">
        <div class="footerimg d-flex mt-0 mb-0">
          <!-- <div class="d-flex footerimg-l mb-0">
            <img
              src="http://via.placeholder.com/50x50"
              alt="image"
              class="avatar brround  mr-2"
            />
            <h5 class="time-title text-muted p-0 leading-normal mt-1 mb-0">
              Wendy Peake<i
                class="si si-check text-success fs-12 ml-1"
                data-toggle="tooltip"
                data-placement="top"
                title="verified"
              ></i>
            </h5>
          </div> -->
          <div class="mt-2 footerimg-r ml-auto">
            <small class="text-muted">{{
              $moment('2019-09-16T14:15:13.253')
            }}</small>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: 'PropertyCard',
  props: ['property'],
  created() {
    this.property.type = this.$store.getters.propertyTypes.filter(
      (i) => i.id === this.property.propertyTypeId
    )[0].name
    this.property.label = this.$store.getters.propertyLabels.filter(
      (i) => i.id === this.property.propertyLabelId
    )[0].name
    this.property.status = this.$store.getters.propertyStatuses.filter(
      (i) => i.id === this.property.propertyStatusId
    )[0].name
  }
}
</script>

<style scoped>
.card-footer .footerimg .footerimg-l .avatar {
  width: 25% !important;
}
.prop-card-icon:hover {
  cursor: pointer;
  color: #24abe3;
}
</style>
