<template>
  <div class="header-main">
    <div class="top-bar">
      <div class="container">
        <div class="row">
          <div class="col-xl-8 col-lg-8 col-sm-4 col-7">
            <div class="top-bar-left d-flex">
              <div class="clearfix">
                <ul class="socials">
                  <li>
                    <a class="social-icon text-dark" href="#"
                      ><i class="fa fa-facebook"></i
                    ></a>
                  </li>
                  <li>
                    <a class="social-icon text-dark" href="#"
                      ><i class="fa fa-twitter"></i
                    ></a>
                  </li>
                  <li>
                    <a class="social-icon text-dark" href="#"
                      ><i class="fa fa-linkedin"></i
                    ></a>
                  </li>
                  <li>
                    <a class="social-icon text-dark" href="#"
                      ><i class="fa fa-google-plus"></i
                    ></a>
                  </li>
                </ul>
              </div>
              <div class="clearfix">
                <ul class="contact">
                  <li class="mr-5 d-lg-none">
                    <a class="callnumber text-dark"
                      ><span
                        ><i class="fa fa-phone mr-1"></i>: +425 345 8765</span
                      ></a
                    >
                  </li>
                  <li
                    v-on:click="langDisplay = !langDisplay"
                    class="dropdown mr-5 lang-curser"
                  >
                    <a class="text-dark" data-toggle="dropdown"
                      ><span>
                        Language <i class="fa fa-caret-down text-muted"></i
                      ></span>
                    </a>
                    <div
                      :class="{ displayLangList: langDisplay }"
                      class="dropdown-menu dropdown-menu-left dropdown-menu-arrow"
                    >
                      <a
                        v-for="(language, index) in languages"
                        :key="index"
                        class="dropdown-item"
                        v-on:click="changeLang(language.urlCode)"
                      >
                        {{ language.name }}
                      </a>
                    </div>
                  </li>
                </ul>
              </div>
              <!-- <nav class="horizontalMenu clearfix d-md-flex">
                <ul class="horizontalMenu-list">
                  <li aria-haspopup="true">
                    <a href="#" class="active"
                      >Home <span class="fa fa-caret-down m-0"></span
                    ></a>
                    <ul class="sub-menu">
                      <a
                        v-for="(language, index) in languages"
                        :key="index"
                        class="dropdown-item "
                        @click="changeLang(language.urlCode)"
                      >
                        {{ language.name }}
                      </a>
                    </ul>
                  </li>
                </ul>
              </nav> -->
              <!-- TODO: comeBack -->
            </div>
          </div>
          <div class="col-xl-4 col-lg-4 col-sm-8 col-5">
            <div class="top-bar-right">
              <ul class="custom">
                <!-- TODO: since we have the log out in the dashboard section we have deleted it from here   -->
                <li v-if="user" class="lang-curser">
                  <a class="text-dark" @click="logOut">
                    <i class="fa fa-sign-out"></i> <span>Log out</span>
                  </a>
                </li>
                <!-- TODO: the above is for the log out section -->
                <li v-if="!user">
                  <nuxt-link
                    :to="
                      $route.params.lang
                        ? `/${$route.params.lang}/Register`
                        : '/Register'
                    "
                    class="text-dark"
                  >
                    <i class="fa fa-user mr-1"></i>
                    <span>Register</span>
                  </nuxt-link>
                  <!-- TODO: before bellow after above -->
                  <!-- <langLink v-if="!user" to="/Register" class="text-dark"
                    ><i class="fa fa-user mr-1"></i>
                    <span>Register</span></langLink
                  > -->
                </li>
                <li v-if="!user">
                  <nuxt-link
                    :to="
                      $route.params.lang
                        ? `/${$route.params.lang}/login`
                        : '/login'
                    "
                    class="text-dark"
                  >
                    <i class="fa fa-sign-in mr-1"></i>
                    <span>Login</span>
                  </nuxt-link>
                  <!-- <langLink v-if="!user" to="/Login" class="text-dark"
                    ><i class="fa fa-sign-in mr-1"></i>
                    <span>Login</span></langLink -->
                </li>
                <li v-if="user">
                  <nuxt-link
                    :to="
                      $route.params.lang
                        ? `/${$route.params.lang}/profile`
                        : '/profile'
                    "
                    class="text-dark"
                  >
                    <i class="fa fa-home mr-1"></i>
                    <span>My Dashboard</span>
                  </nuxt-link>
                </li>

                <li class="dropdown">
                  <!-- <langLink
                    to="/profile"
                    class="text-dark"
                    data-toggle="dropdown"
                    ><i class="fa fa-home mr-1"></i>
                    <span>My Dashboard</span></langLink -->

                  <div
                    class="dropdown-menu dropdown-menu-right dropdown-menu-arrow"
                  >
                    <a href="mydash.html" class="dropdown-item">
                      <i class="dropdown-icon icon icon-user"></i> My Profile
                    </a>
                    <a class="dropdown-item" href="#">
                      <i class="dropdown-icon icon icon-speech"></i> Inbox
                    </a>
                    <a class="dropdown-item" href="#">
                      <i class="dropdown-icon icon icon-bell"></i> Notifications
                    </a>
                    <a href="mydash.html" class="dropdown-item">
                      <i class="dropdown-icon  icon icon-settings"></i> Account
                      Settings
                    </a>
                    <a class="dropdown-item" href="#">
                      <i class="dropdown-icon icon icon-power"></i> Log out
                    </a>
                  </div>
                </li>
              </ul>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import Cookie from 'js-cookie'
export default {
  name: 'TopHeader',
  data() {
    return {
      langDisplay: false,
      languages: [],
      selectedLanguage: null
    }
  },
  computed: {
    user() {
      return this.$store.getters.user
    }
  },
  beforeDestroy() {
    document.removeEventListener('click', this.close)
  },
  created() {
    this.languages = this.$store.getters.languages
    this.selectedLanguage = this.$store.getters.selectedLanguage
  },
  methods: {
    changeLang(lang) {
      let newRoute = this.$route.fullPath
      if (this.$route.params.lang) newRoute = newRoute.substr(3)
      if (lang.toLowerCase() === 'en') location.replace(newRoute)
      else location.replace(`/${lang}${newRoute}`)
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

<style scoped>
.displayLangList {
  display: block;
}
.lang-curser {
  cursor: pointer;
}
</style>
