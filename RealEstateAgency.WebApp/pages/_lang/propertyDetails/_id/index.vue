<template>
  <section class="sptb">
    <div class="container">
      <div class="row">
        <div class="col-xl-8 col-lg-8 col-md-12">
          <!--Classified Description-->
          <div class="card overflow-hidden">
            <div class="ribbon ribbon-top-right text-danger">
              <span class="bg-danger">{{ propertyDetails.propertyLabel }}</span>
            </div>
            <div class="card-body">
              <div class="item-det mb-4">
                <a class="text-dark"
                  ><h3 class="">
                    {{ propertyDetails.title }}
                  </h3></a
                >
                <ul class="d-flex">
                  <li class="mr-5">
                    <a class="icons"
                      ><i class="icon icon-briefcase text-muted mr-1"></i>
                      RealEstate</a
                    >
                  </li>
                  <li class="mr-5">
                    <a class="icons"
                      ><i class="icon icon-location-pin text-muted mr-1"></i>
                      {{ propertyDetails.propertyLocation.city }}</a
                    >
                  </li>
                  <li class="mr-5">
                    <a class="icons"
                      ><i class="icon icon-calendar text-muted mr-1"></i> 5
                      hours ago</a
                    >
                  </li>
                  <li class="mr-5">
                    <a class="icons"
                      ><i class="icon icon-eye text-muted mr-1"></i> 765</a
                    >
                  </li>
                  <li class="">
                    <a class="icons">
                      <i class="fa fa-star text-warning"></i>
                      <i class="fa fa-star text-warning"></i>
                      <i class="fa fa-star text-warning"></i>
                      <i class="fa fa-star text-warning"></i>
                      <i class="fa fa-star-half-o text-warning mr-1"></i>4.5</a
                    >
                  </li>
                </ul>
              </div>
              <div class="product-slider">
                <div id="carousel" class="carousel slide" data-ride="carousel">
                  <div class="arrow-ribbon2 bg-primary">
                    ${{ propertyDetails.price.price }}
                  </div>
                  <div class="carousel-inner">
                    <div class="carousel-item active">
                      <img
                        src="https://i2.au.reastatic.net/800x600/23a7f7e2348813a41fa9bbd8f6e73aa8c54ab7f041df408c1e23a436c49cc9e6/image.jpg"
                        alt="img"
                      />
                    </div>
                    <!--                    <div class="carousel-item">-->
                    <!--                      <img-->
                    <!--                        src="~assets/images/products/pictures/h2.jpg"-->
                    <!--                        alt="img"-->
                    <!--                      />-->
                    <!--                    </div>-->
                  </div>
                  <a
                    class="carousel-control-prev"
                    role="button"
                    data-slide="prev"
                  >
                    <i class="fa fa-angle-left" aria-hidden="true"></i>
                  </a>
                  <a
                    class="carousel-control-next"
                    role="button"
                    data-slide="next"
                  >
                    <i class="fa fa-angle-right" aria-hidden="true"></i>
                  </a>
                </div>
                <!--                <div class="clearfix">-->
                <!--                  <div-->
                <!--                    id="thumbcarousel"-->
                <!--                    class="carousel slide"-->
                <!--                    data-interval="false"-->
                <!--                  >-->
                <!--                    <div class="carousel-inner">-->
                <!--                      <div class="carousel-item active">-->
                <!--                        <div-->
                <!--                          data-target="#carousel"-->
                <!--                          data-slide-to="0"-->
                <!--                          class="thumb"-->
                <!--                        >-->
                <!--                          <img src="~assets/images/products/h3.png" alt="img" />-->
                <!--                        </div>-->
                <!--                        <div-->
                <!--                          data-target="#carousel"-->
                <!--                          data-slide-to="1"-->
                <!--                          class="thumb"-->
                <!--                        >-->
                <!--                          <img src="~assets/images/products/h2.png" alt="img" />-->
                <!--                        </div>-->
                <!--                        <div-->
                <!--                          data-target="#carousel"-->
                <!--                          data-slide-to="2"-->
                <!--                          class="thumb"-->
                <!--                        >-->
                <!--                          <img src="~assets/images/products/h6.png" alt="img" />-->
                <!--                        </div>-->
                <!--                        <div-->
                <!--                          data-target="#carousel"-->
                <!--                          data-slide-to="3"-->
                <!--                          class="thumb"-->
                <!--                        >-->
                <!--                          <img src="~assets/images/products/h5.png" alt="img" />-->
                <!--                        </div>-->
                <!--                        <div-->
                <!--                          data-target="#carousel"-->
                <!--                          data-slide-to="4"-->
                <!--                          class="thumb"-->
                <!--                        >-->
                <!--                          <img src="~assets/images/products/h4.png" alt="img" />-->
                <!--                        </div>-->
                <!--                      </div>-->
                <!--                      <div class="carousel-item">-->
                <!--                        <div-->
                <!--                          data-target="#carousel"-->
                <!--                          data-slide-to="0"-->
                <!--                          class="thumb"-->
                <!--                        >-->
                <!--                          <img src="~assets/images/products/h1.png" alt="img" />-->
                <!--                        </div>-->
                <!--                        <div-->
                <!--                          data-target="#carousel"-->
                <!--                          data-slide-to="1"-->
                <!--                          class="thumb"-->
                <!--                        >-->
                <!--                          <img src="~assets/images/products/h2.png" alt="img" />-->
                <!--                        </div>-->
                <!--                        <div-->
                <!--                          data-target="#carousel"-->
                <!--                          data-slide-to="2"-->
                <!--                          class="thumb"-->
                <!--                        >-->
                <!--                          <img src="~assets/images/products/h6.png" alt="img" />-->
                <!--                        </div>-->
                <!--                        <div-->
                <!--                          data-target="#carousel"-->
                <!--                          data-slide-to="3"-->
                <!--                          class="thumb"-->
                <!--                        >-->
                <!--                          <img src="~assets/images/products/h5.png" alt="img" />-->
                <!--                        </div>-->
                <!--                        <div-->
                <!--                          data-target="#carousel"-->
                <!--                          data-slide-to="4"-->
                <!--                          class="thumb"-->
                <!--                        >-->
                <!--                          <img src="~assets/images/products/h4.png" alt="img" />-->
                <!--                        </div>-->
                <!--                      </div>-->
                <!--                    </div>-->
                <!--                    <a-->
                <!--                      class="carousel-control-prev"-->
                <!--                      role="button"-->
                <!--                      data-slide="prev"-->
                <!--                    >-->
                <!--                      <i class="fa fa-angle-left" aria-hidden="true"></i>-->
                <!--                    </a>-->
                <!--                    <a-->
                <!--                      class="carousel-control-next"-->
                <!--                      role="button"-->
                <!--                      data-slide="next"-->
                <!--                    >-->
                <!--                      <i class="fa fa-angle-right" aria-hidden="true"></i>-->
                <!--                    </a>-->
                <!--                  </div>-->
                <!--                </div>-->
              </div>
            </div>
          </div>
          <!--          <Panorama />-->
          <div class="border-0">
            <div class="wideget-user-tab wideget-user-tab3">
              <div class="tab-menu-heading">
                <div class="tabs-menu1">
                  <ul class="nav">
                    <li
                      style="cursor: pointer;"
                      v-for="(tab, index) in tabs"
                      :key="index"
                    >
                      <a
                        @click="changeComponentDetail(tab.component)"
                        :class="{
                          activeTab: currentDetailComponent === tab.component
                        }"
                        data-toggle="tab"
                        >{{ tab.component }}</a
                      >
                      <!--                    </li>-->
                      <!--                    <li>-->
                      <!--                      <a-->
                      <!--                        @click="changeComponentDetail('PropertyLocation')"-->
                      <!--                        data-toggle="tab"-->
                      <!--                        class=""-->
                      <!--                        >Location</a-->
                      <!--                      >-->
                      <!--                    </li>-->
                      <!--                    <li>-->
                      <!--                      <a-->
                      <!--                        @click="changeComponentDetail('PropertyDetail')"-->
                      <!--                        data-toggle="tab"-->
                      <!--                        class=""-->
                      <!--                        >Details</a-->
                      <!--                      >-->
                      <!--                    </li>-->
                      <!--                    <li>-->
                      <!--                      <a-->
                      <!--                        @click="changeComponentDetail('PropertyFeatures')"-->
                      <!--                        data-toggle="tab"-->
                      <!--                        class=""-->
                      <!--                        >Features</a-->
                      <!--                      >-->
                      <!--                    </li>-->
                    </li>
                  </ul>
                </div>
              </div>
            </div>
            <div class="tab-content border-0 p-5 bg-white details-tab-content">
              <div class="tab-pane active">
                <component :is="currentDetailComponent"></component>
              </div>
              <div class="tab-pane" id="tab-4">
                <div class="row">
                  <div class="col-md-12">
                    <div class="table-responsive">
                      <table class="table table-bordered border-top mb-0">
                        <tbody>
                          <tr>
                            <td>Type</td>
                            <td>
                              <span class="font-weight-bold">2BHK Flat</span>
                            </td>
                          </tr>
                          <tr>
                            <td>Bathrooms</td>
                            <td><span class="font-weight-bold">2</span></td>
                          </tr>
                          <tr>
                            <td>Carpet Area</td>
                            <td>
                              <span class="font-weight-bold">1278 fts</span>
                            </td>
                          </tr>
                          <tr>
                            <td>Facing</td>
                            <td>
                              <span class="font-weight-bold">East Face</span>
                            </td>
                          </tr>
                          <tr>
                            <td>Bed Rooms</td>
                            <td><span class="font-weight-bold">2</span></td>
                          </tr>
                          <tr>
                            <td>Furnishing</td>
                            <td>
                              <span class="font-weight-bold">Unfurnished</span>
                            </td>
                          </tr>
                          <tr>
                            <td>Balcony</td>
                            <td><span class="font-weight-bold">2</span></td>
                          </tr>
                          <tr>
                            <td>Lift</td>
                            <td><span class="font-weight-bold">Yes</span></td>
                          </tr>
                          <tr>
                            <td>Swimming pool</td>
                            <td><span class="font-weight-bold">Yes</span></td>
                          </tr>
                          <tr>
                            <td>Garden</td>
                            <td><span class="font-weight-bold">Yes</span></td>
                          </tr>
                        </tbody>
                      </table>
                    </div>
                  </div>
                </div>
              </div>
              <div class="tab-pane" id="tab-5">
                <ul class="list-unstyled video-list-thumbs row ">
                  <li class="mb-0">
                    <a data-toggle="modal" data-target="#homeVideo">
                      <img
                        src="~assets/images/products/pictures/h1.jpg"
                        alt="Barca"
                        class="img-responsive"
                      />
                      <span
                        class="mdi mdi-arrow-right-drop-circle-outline text-white"
                      ></span>
                    </a>
                  </li>
                </ul>
              </div>
            </div>
            <div class="pt-4 pb-4 pl-5 pr-5 border-top bg-white">
              <div class="list-id">
                <div class="row">
                  <div class="col">
                    <a class="mb-0"
                      >Unique Id: {{ propertyDetails.propertyUniqId }}</a
                    >
                  </div>
                  <div class="col col-auto">
                    Posted By <a class="mb-0 font-weight-bold">Individual</a> /
                    21st Dec 2019
                  </div>
                </div>
              </div>
            </div>
            <div class="card-footer bg-white details-tab">
              <div class="icons">
                <a class="btn btn-info icons btn-apply"
                  ><i class="icon icon-share mr-1 btn-apply"></i> Share Ad</a
                >
                <a class="btn btn-secondary icons btn-apply"
                  ><i class="icon icon-printer  mr-1 btn-apply"></i> Print</a
                >
              </div>
            </div>
          </div>
          <div class="card mb-lg-0 mt-2">
            <div class="card-header">
              <h3 class="card-title">Apply for this property</h3>
            </div>
            <div class="card-body">
              <div>
                <div class="form-group">
                  <input
                    type="text"
                    class="form-control"
                    id="name1"
                    placeholder="Your Name"
                  />
                </div>
                <div class="form-group">
                  <input
                    type="email"
                    class="form-control"
                    id="email"
                    placeholder="Email Address"
                  />
                </div>
                <div class="form-group">
                  <textarea
                    class="form-control"
                    name="example-textarea-input"
                    rows="6"
                    placeholder="Description"
                  ></textarea>
                </div>
                <a class="btn btn-primary btn-apply">Apply</a>
              </div>
            </div>
          </div>
        </div>

        <!--Right Side Content-->
        <div class="col-xl-4 col-lg-4 col-md-12">
          <div class="card">
            <div class="card-header">
              <h3 class="card-title">Posted By</h3>
            </div>
            <div class="card-body  item-user">
              <div class="profile-pic mb-0">
                <img
                  src="https://i2.au.reastatic.net/120x160/e390fbbdaf1679acaf0323ababafe840cf63315b26ebe48c17af8cefed0b938f/main.jpg"
                  class="brround avatar-xxl"
                  alt="user"
                />
                <div class="">
                  <a class="text-dark"
                    ><h4 class="mt-3 mb-1 font-weight-semibold">
                      Robert McLean
                    </h4></a
                  >
                  <p class="mb-0">RealEstate Agent</p>
                  <span class="text-muted">Member Since November 2008</span>
                </div>
              </div>
            </div>
            <div class="card-body item-user">
              <h4 class="mb-4">Contact Info</h4>
              <div>
                <h6>
                  <span class="font-weight-semibold"
                    ><i class="fa fa-map-marker mr-2 mb-2"></i></span
                  ><a class="text-body"> 7981 Aspen Ave. Hammonton, USA</a>
                </h6>
                <h6>
                  <span class="font-weight-semibold"
                    ><i class="fa fa-envelope mr-2 mb-2"></i></span
                  ><a class="text-body"> robert123@gmail.com</a>
                </h6>
                <h6>
                  <span class="font-weight-semibold"
                    ><i class="fa fa-phone mr-2  mb-2"></i></span
                  ><a class="text-body"> 0-235-657-24587</a>
                </h6>
                <h6>
                  <span class="font-weight-semibold"
                    ><i class="fa fa-link mr-2 "></i></span
                  ><a class="text-body">http://spruko.com/</a>
                </h6>
              </div>
              <div class=" item-user-icons mt-4">
                <a class="facebook-bg mt-0"
                  ><i class="fa fa-facebook btn-apply"></i
                ></a>
                <a class="twitter-bg btn-apply"
                  ><i class="fa fa-twitter"></i
                ></a>
                <a class="google-bg btn-apply"><i class="fa fa-google"></i></a>
                <a class="dribbble-bg btn-apply"
                  ><i class="fa fa-dribbble"></i
                ></a>
              </div>
            </div>
            <div class="card-footer">
              <div class="text-left">
                <a
                  class="btn btn-primary pointer btn-apply"
                  data-toggle="modal"
                  data-target="#contact"
                  ><i class="fa fa-user"></i> Contact Me</a
                >
                <a class="btn btn-info pointer btn-apply"
                  ><i class="fa fa-share"></i> Share</a
                >
              </div>
            </div>
          </div>
          <div class="card">
            <div class="card-header">
              <h3 class="card-title">Keywords</h3>
            </div>
            <div class="card-body product-filter-desc">
              <div class="product-tags clearfix">
                <ul class="list-unstyled mb-0">
                  <li>
                    <a class="pointer">{{ propertyTypes[1].name }}</a>
                  </li>
                  <li>
                    <a class="pointer">{{ propertyTypes[2].name }}</a>
                  </li>
                  <li>
                    <a class="pointer">{{ propertyTypes[3].name }}</a>
                  </li>
                </ul>
              </div>
            </div>
          </div>
          <div class="card">
            <div class="card-header">
              <h3 class="card-title">Shares</h3>
            </div>
            <div class="card-body product-filter-desc">
              <div class="product-filter-icons text-center">
                <a class="facebook-bg btn-apply"
                  ><i class="fa fa-facebook"></i
                ></a>
                <a class="twitter-bg btn-apply"
                  ><i class="fa fa-twitter"></i
                ></a>
                <a class="google-bg btn-apply"><i class="fa fa-google"></i></a>
                <a class="dribbble-bg btn-apply"
                  ><i class="fa fa-dribbble"></i
                ></a>
                <a class="pinterest-bg btn-apply"
                  ><i class="fa fa-pinterest"></i
                ></a>
              </div>
            </div>
          </div>
          <div class="card">
            <div class="card-header">
              <h3 class="card-title">Map location</h3>
            </div>
            <div class="card-body">
              <div class="map-header">
                <div class="map-header-layer">
                  <img src="~/static/images/Capture.jpg" />
                </div>
              </div>
            </div>
          </div>
          <div class="card">
            <div class="card-header">
              <h3 class="card-title">Search Ads</h3>
            </div>
            <div class="card-body">
              <div class="form-group">
                <input
                  type="text"
                  class="form-control"
                  id="text2"
                  placeholder="What are you looking for?"
                />
              </div>
              <div class="form-group">
                <select
                  name="country"
                  id="select-countries"
                  class="form-control custom-select select2-show-search"
                >
                  <option
                    v-for="(type, index) in propertyTypes"
                    :key="index"
                    value="1"
                    selected
                    >{{ type.name }}</option
                  >
                </select>
              </div>
              <div class="">
                <a class="btn  btn-primary btn-apply">Search</a>
              </div>
            </div>
          </div>
        </div>
        <!--/Right Side Content-->
      </div>
    </div>
  </section>
</template>

<script>
import Overview from '../../../../components/property/propertyDetail/Overview'
import PropertyLocation from '../../../../components/property/propertyDetail/Location'
import PropertyDetail from '../../../../components/property/propertyDetail/Details'
import PropertyFeatures from '../../../../components/property/propertyDetail/Features'
// import Panorama from '../../../../components/property/propertyDetail/Panorama'

export default {
  data() {
    return {
      currentDetailComponent: 'Overview',
      tabs: [
        { component: 'Overview' },
        { component: 'PropertyLocation' },
        { component: 'PropertyDetail' },
        { component: 'PropertyFeatures' }
      ],
      propertyTypes: []
    }
  },
  components: {
    Overview,
    PropertyLocation,
    PropertyDetail,
    PropertyFeatures
    // Panorama
  },
  methods: {
    changeComponentDetail(item) {
      this.currentDetailComponent = item
      debugger
    }
  },

  async asyncData({ $axios, store, params }) {
    // const id = context.params.id
    const getPropertyDetails = await $axios.$get(
      `property/getWebAppEstate/${params.id}`
    )
    getPropertyDetails.propertyLabel = store.state.propertyLabels.filter(
      (i) => i.id === getPropertyDetails.propertyLabelId
    )[0].name
    getPropertyDetails.propertyType = store.state.propertyTypes.filter(
      (i) => i.id === getPropertyDetails.propertyTypeId
    )[0].name
    getPropertyDetails.propertyStatus = store.state.propertyStatuses.filter(
      (i) => i.id === getPropertyDetails.propertyStatusId
    )[0].name
    getPropertyDetails.propertyLocation.city = store.state.cities.filter(
      (i) => i.id === getPropertyDetails.propertyLocation.cityId
    )[0].name
    store.commit('SET_PROPERTY_DETAIL', getPropertyDetails)

    return {
      propertyDetails: getPropertyDetails
    }
  },
  created() {
    this.propertyTypes = this.$store.getters.propertyTypes
  }
}
</script>

<style>
.title {
  font-family: 'Quicksand', 'Source Sans Pro', -apple-system, BlinkMacSystemFont,
    'Segoe UI', Roboto, 'Helvetica Neue', Arial, sans-serif;
  display: block;
  font-weight: 300;
  font-size: 100px;
  color: #35495e;
  letter-spacing: 1px;
}
.activeTab {
  background-color: white !important;
}
.pointer {
  cursor: pointer !important;
}
.btn-apply {
  color: white !important;
}
</style>
