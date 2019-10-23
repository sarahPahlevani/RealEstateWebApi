<template>
  <section class="sptb">
    <div class="container">
      <div class="row">
        <div class="col-xl-3 col-lg-12 col-md-12">
          <div class="card">
            <div class="card-header">
              <h3 class="card-title">My Dashboard</h3>
            </div>
            <div class="card-body text-center item-user border-bottom">
              <div class="profile-pic">
                <div class="profile-pic-img">
                  <span
                    class="bg-success dots"
                    data-toggle="tooltip"
                    data-placement="top"
                    title="online"
                  ></span>
                  <img
                    src="../../assets/images/faces/male/25.jpg"
                    class="brround"
                    alt="user"
                  />
                </div>
                <a class="text-dark"
                  ><h4 class="mt-3 mb-0 font-weight-semibold">
                    {{ userInfo.firstName }}
                  </h4></a
                >
              </div>
            </div>
            <div class="item1-links mb-0 items">
              <a
                @click="changeDashboardSection('EditProfile')"
                class="d-flex border-bottom"
                :class="{ activeComponent: currentComponent === 'EditProfile' }"
              >
                <span class="icon1 mr-3"><i class="icon icon-user"></i></span>
                <span> Edit Profile</span>
              </a>
              <a
                @click="changeDashboardSection('MyAds')"
                class=" d-flex  border-bottom"
                :class="{ activeComponent: currentComponent === 'MyAds' }"
              >
                <span class="icon1 mr-3"
                  ><i class="icon icon-diamond"></i
                ></span>
                <span> My Ads</span>
              </a>
              <a
                @click="changeDashboardSection('MyFaves')"
                class=" d-flex border-bottom"
                :class="{ activeComponent: currentComponent === 'MyFaves' }"
              >
                <span class="icon1 mr-3"><i class="icon icon-heart"></i></span>
                <span>My Favorite</span>
              </a>

              <a
                @click="changeDashboardSection('PublishedAds')"
                class=" d-flex  border-bottom"
                :class="{
                  activeComponent: currentComponent === 'PublishedAds'
                }"
              >
                <span class="icon1 mr-3"
                  ><i class="icon icon-credit-card"></i
                ></span>
                <span> My Published Ads</span>
              </a>
              <!-- <a
                @click="changeDashboardSection('Settings')"
                class="d-flex border-bottom"
              >
                <span class="icon1 mr-3"
                  ><i class="icon icon-settings"></i
                ></span>
                Settings
              </a> -->
              <a @click="logOut" class="d-flex">
                <span class="icon1 mr-3"><i class="icon icon-power"></i></span>
                Logout
              </a>
            </div>
          </div>
          <div class="card mb-xl-0">
            <div class="card-header">
              <h3 class="card-title">Safety Tips For Buyers</h3>
            </div>
            <div class="card-body">
              <ul class="list-unstyled widget-spec  mb-0">
                <li class="">
                  <i class="fa fa-check text-success" aria-hidden="true"></i>
                  Meet Seller at public Place
                </li>
                <li class="">
                  <i class="fa fa-check text-success" aria-hidden="true"></i>
                  Check item before you buy
                </li>
                <li class="">
                  <i class="fa fa-check text-success" aria-hidden="true"></i>
                  Pay only after collecting item
                </li>
                <li class="ml-5 mb-0">
                  <a href="tips.html"> View more..</a>
                </li>
              </ul>
            </div>
          </div>
        </div>
        <div class="col-xl-9 col-lg-12 col-md-12">
          <component :is="currentComponent"></component>
        </div>
      </div>
    </div>
  </section>
</template>

<script>
import Cookie from 'js-cookie'
import EditProfile from '~/components/profile/EditProfile'
import MyAds from '~/components/profile/MyAds'
import PublishedAds from '~/components/profile/PublishedAds'
import MyFaves from '~/components/profile/MyFaves'
import Settings from '~/components/profile/Settings'

export default {
  middleware: 'authenticated',
  components: {
    EditProfile,
    MyAds,
    PublishedAds,
    MyFaves,
    Settings
  },
  data() {
    return {
      currentComponent: 'EditProfile',
      userInfo: {
        userName: 'string',
        isConfirmed: true,
        registrationDate: '2019-10-03T15:42:08.053Z',
        firstName: 'string',
        lastName: 'string',
        middleName: 'string',
        email: 'string',
        phone01: 'string',
        phone02: 'string',
        address01: 'string',
        address02: 'string',
        zipCode: 'string',
        userPicture: 'string',
        userPictureTumblr: 'string'
      }
    }
  },
  computed: {
    user() {
      return this.$store.getters.user
    }
  },
  async asyncData({ store, $axios }) {
    try {
      const res = await $axios.$get('/useraccount/profile', {
        headers: {
          Authorization: `Bearer ${store.getters.getToken}`
        }
      })
      store.commit('SET_USER_PROFILE', res)
      return {
        userInfo: res
      }
    } catch (err) {
      console.log(err)
    }
  },
  created() {},
  methods: {
    changeDashboardSection(section) {
      this.currentComponent = section
    },
    logOut() {
      Cookie.remove('token')
      this.$store.commit('SET_USER', null)
      this.$store.commit('SET_TOKEN', null)
      location.reload(true)
    }
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
.activeComponent {
  background: rgba(241, 238, 247, 0.6);
  color: #24abe3;
  border-right: 2px solid #24abe3;
}
.activeComponent span {
  color: #24abe3;
}
.items a {
  cursor: pointer;
}
</style>
