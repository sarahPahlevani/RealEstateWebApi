<template>
  <div class="whole">
    <section class="banner-1 cover-image sptb-2 sptb-tab bg-background2 ">
      <!-- <img src="~/assets/images/banners/header.jpg" alt="" /> -->
      <div class="header-text text mb-0">
        <div class="container">
          <div class="text-center text-white ">
            <h1 class="">Find Your Best Property</h1>
          </div>
          <div class="row">
            <div class="col-xl-10 col-lg-12 col-md-12 d-block mx-auto">
              <div class="item-search-tabs">
                <div class="item-search-menu">
                  <ul class="nav">
                    <li class="">
                      <a
                        @click="changeType('rent')"
                        :class="{ active: rentActive }"
                        class="filterCurser"
                        data-toggle="tab"
                        >For Rent</a
                      >
                    </li>
                    <li>
                      <a
                        @click="changeType('sale')"
                        :class="{ active: saleActive }"
                        class="filterCurser"
                        data-toggle="tab"
                        >For Sale</a
                      >
                    </li>
                  </ul>
                </div>
                <div class="tab-content index-search-select">
                  <div class="tab-pane active" id="index1">
                    <div class=" search-background">
                      <div class="form row no-gutters">
                        <div
                          class="form-group  col-xl-5 col-lg-5 col-md-12 mb-0"
                        >
                          <input
                            type="text"
                            class="form-control input-lg br-tr-md-0 br-br-md-0 dastan"
                            id="text4"
                            placeholder="Enter Title"
                          />
                          <span
                            ><i class="fa fa-file-text-o location-gps mr-1"></i
                          ></span>
                        </div>
                        <div
                          class="form-group col-xl-3 col-lg-3 col-md-12 mb-0"
                        >
                          <select
                            class="form-control select2-show-search border-bottom-0 filterCurser dastan"
                            data-placeholder="Property Type"
                          >
                            <optgroup>
                              <option disabled selected>Property Type</option>
                              <option
                                v-for="(type, index) in propertyTypes"
                                :value="type.id"
                                :key="index"
                                >{{ type.name }}</option
                              >
                            </optgroup>
                          </select>
                        </div>
                        <div
                          class="form-group col-xl-3 col-lg-3 col-md-12 mb-0 dastan"
                        >
                          <select
                            class="form-control select2-show-search filterCurser border-bottom-0 dastan"
                            data-placeholder="Max Value"
                          >
                            <option disabled selected>Property Labels</option>
                            <option
                              v-for="(label, index) in propertyLabels"
                              :value="label.id"
                              :key="index"
                              >{{ label.name }}</option
                            >
                          </select>
                        </div>

                        <div class="col-xl-1 col-lg-1 col-md-12 mb-0">
                          <a
                            href="#"
                            class="btn btn-block btn-primary fs-14 dastan"
                            ><i class="fa fa-search"></i
                          ></a>
                        </div>
                      </div>
                    </div>
                  </div>
                  <div class="tab-pane" id="index2">
                    <div class=" search-background">
                      <div class="form row no-gutters">
                        <div
                          class="form-group  col-xl-4 col-lg-3 col-md-12 mb-0"
                        >
                          <input
                            type="text"
                            class="form-control border"
                            id="employe-text2"
                            placeholder="Enter Your Location"
                          />
                          <span
                            ><i class="fa fa-map-marker location-gps mr-1"></i
                          ></span>
                        </div>
                        <div
                          class="form-group col-xl-3 col-lg-3 col-md-12 mb-0"
                        >
                          <select
                            class="form-control border-bottom-0  select2-show-search"
                            data-placeholder="Select"
                          >
                            <optgroup label="Categories">
                              <option>Property Type</option>
                              <option value="1">Deluxe Houses</option>
                              <option value="2">Modren Flats</option>
                              <option value="3">Apartments</option>
                              <option value="4">Stylish Houses</option>
                              <option value="5">Offices Houses</option>
                              <option value="6">Laxury Houses</option>
                              <option value="7">Nature Houses</option>
                            </optgroup>
                          </select>
                        </div>
                        <div
                          class="form-group col-xl-1 col-lg-1 col-md-12  mb-0"
                        >
                          <select
                            class="form-control  select2-show-search  border-bottom-0"
                            data-placeholder="Bed Rooms"
                          >
                            <option value="1" selected>Bed</option>
                            <option value="2">1</option>
                            <option value="3">2</option>
                            <option value="4">3</option>
                            <option value="5">4</option>
                            <option value="5">5</option>
                          </select>
                        </div>
                        <div
                          class="form-group col-xl-1 col-lg-1 col-md-12 mb-0 "
                        >
                          <select
                            class="form-control select2-show-search  border-bottom-0"
                            data-placeholder="Bath Rooms"
                          >
                            <option value="1" selected>Bath</option>
                            <option value="2">1</option>
                            <option value="3">2</option>
                            <option value="4">3</option>
                            <option value="5">4</option>
                            <option value="5">5</option>
                          </select>
                        </div>
                        <div
                          class="form-group col-xl-1 col-lg-1 col-md-12  mb-0 "
                        >
                          <select
                            class="form-control select2-show-search  border-bottom-0"
                            data-placeholder="Min Value"
                          >
                            <option value="1" selected>Min</option>
                            <option value="2">$50</option>
                            <option value="3">$60</option>
                            <option value="4">$70</option>
                            <option value="5">$80</option>
                            <option value="5">$90</option>
                          </select>
                        </div>
                        <div
                          class="form-group col-xl-1 col-lg-1 col-md-12  mb-0 "
                        >
                          <select
                            class="form-control select2-show-search  border-bottom-0"
                            data-placeholder="Max Value"
                          >
                            <option value="1" selected>Max</option>
                            <option value="2">$100</option>
                            <option value="3">$110</option>
                            <option value="4">$120</option>
                            <option value="5">$130</option>
                            <option value="5">$140</option>
                          </select>
                        </div>
                        <div class="col-xl-1 col-lg-2 col-md-12 mb-0">
                          <a href="#" class="btn btn-block btn-primary fs-14"
                            ><i class="fa fa-search"></i
                          ></a>
                        </div>
                      </div>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
      <!-- /header-text -->
    </section>
    <!-- <section>
      <div
        class="banner-1 cover-image sptb-2 sptb-tab bg-background2 "
        data-image-src="https://dummyimage.com/600x400/000/fff&text=FakeImg"
      >
        <div class="header-text mb-0 animated slideInLeft delay-1s">
          <div class="container">
            <div class="text-center text-white mb-7">
              <h1 class="mb-1">Find Your Best Property</h1>
              <p>
                Use the filter bellow to find the best property that suits your
                desire.
              </p>
            </div>
            <div class="row">
              <div class="col-xl-10 col-lg-12 col-md-12 d-block mx-auto">
                <div class="search-background bg-transparent">
                  <div class="form row no-gutters ">
                    <div
                      class="form-group col-xl-4 col-lg-4 col-md-12 mb-0 bg-white "
                    >
                      <input
                        type="text"
                        class="form-control input-lg br-tr-md-0 br-br-md-0"
                        id="text4"
                        placeholder="Enter Title"
                      />
                      <span
                        ><i class="fa fa-file-text-o location-gps mr-1"></i
                      ></span>
                    </div>
                    <div
                      class="form-group col-xl-3 col-lg-3 col-md-12 select2-lg mb-0 bg-white"
                    >
                      <select
                        class="form-control select2-show-search border-bottom-0 filterCurser"
                        data-placeholder="Property Type"
                      >
                        <optgroup>
                          <option disabled selected>Property Type</option>
                          <option
                            v-for="(type, index) in propertyTypes"
                            :value="type.id"
                            :key="index"
                            >{{ type.name }}</option
                          >
                        </optgroup>
                      </select>
                    </div>
                    <div
                      class="form-group col-xl-3 col-lg-3 col-md-12 select2-l mb-0 bg-white"
                    >
                      <select
                        class="form-control select2-show-search filterCurser border-bottom-0"
                        data-placeholder="Min Value"
                      >
                        <option disabled value="1" selected
                          >Property Status</option
                        >
                        <option
                          v-for="(status, index) in propertyStatuses"
                          :value="status.id"
                          :key="index"
                          >{{ status.name }}</option
                        >
                      </select>
                    </div>
                    <div
                      class="form-group col-xl-3 col-lg-3 col-md-12 select2-lg mb-0 bg-white"
                    >
                      <select
                        class="form-control select2-show-search filterCurser border-bottom-0"
                        data-placeholder="Max Value"
                      >
                        <option disabled selected>Property Labels</option>
                        <option
                          v-for="(label, index) in propertyLabels"
                          :value="label.id"
                          :key="index"
                          >{{ label.name }}</option
                        >
                      </select>
                    </div>
                    <div class="col-xl-3 col-lg-3 col-md-12 mb-0">
                      <a
                        class="btn btn-lg btn-block btn-primary br-tl-md-0 br-bl-md-0"
                        >Search Here</a
                      >
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </section> -->
  </div></template
>
<script>
export default {
  name: 'FilterSection',
  data() {
    return {
      propertyTypes: [],
      propertyStatuses: [],
      propertyFeatures: [],
      propertyLabels: [],
      rentActive: true,
      saleActive: false
    }
  },
  created() {
    this.propertyTypes = this.$store.getters.propertyTypes
    this.propertyStatuses = this.$store.getters.propertyStatuses
    this.propertyFeatures = this.$store.getters.propertyFeatures
    this.propertyLabels = this.$store.getters.propertyLabels
  },
  methods: {
    changeType(item) {
      switch (item) {
        case 'rent':
          this.saleActive = false
          this.rentActive = true
          break
        case 'sale':
          this.saleActive = true
          this.rentActive = false
          break
      }
    }
  }
}
</script>
<style scoped>
.whole {
  background: url(../../assets/images/banners/header.jpg) no-repeat fixed center;
  background-size: 100% 100%;
}
.filterCurser {
  cursor: pointer;
}
.filterCurser:hover {
  color: #24abe3;
}
.filterCurser:focus {
  color: #24abe3;
}
.dastan {
  height: 46px !important;
}
</style>
