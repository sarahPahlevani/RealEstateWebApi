<template>
  <div class="container-fluid header-whole">
    <div class="container main-header animated fadeInDown delay-1s">
      <div class="left-side-header only-web ">
        <div class="horizontal-mainwrapper container clearfix">
          <nav class="horizontalMenu clearfix d-md-flex">
            <ul class="horizontalMenu-list">
              <li aria-haspopup="true">
                <LangLink class="active" to="/">
                  <span>Home</span>
                </LangLink>
              </li>

              <li aria-haspopup="true">
                <LangLink to="/contact-us"><span>Contact Us </span></LangLink>
              </li>
              <li aria-haspopup="true">
                <LangLink to="/about"><span>Advanced Search </span></LangLink>
              </li>
              <li>
                <LangLink to="/submit-property" class="orange-button"
                  ><span>Submit Property</span></LangLink
                >
              </li>
            </ul>
          </nav>
        </div>
      </div>
      <div class="center-side-header">
        <!-- <span id="horizontal-navtoggle" class="animated-arrow"><span></span></a> -->
        <langLink to="/"
          ><img src="~/static/images/logo.png" width="120" alt=""
        /></langLink>
      </div>
      <div class="right-side-header only-web">
        <div class="horizontal-mainwrapper container ">
          <div class="top-bar-right">
            <ul class="custom">
              <li class="log-reg-dash">
                <langLink v-if="!user" to="/login" class="text-white"
                  ><i class="fa fa-sign-in mr-1"></i>
                  <span>Log In / Sign Up</span></langLink
                >
                <a v-if="user" class="text-white log-out" @click="logOut">
                  <i class="fa fa-sign-out"></i> <span>Log out</span>
                </a>
              </li>
              <li class="dropdown log-reg-dash">
                <langLink
                  to="/profile"
                  class="text-white"
                  data-toggle="dropdown"
                  ><i class="fa fa-user mr-1"></i>
                  <span>Profile</span></langLink
                >
              </li>
              <li
                class="dropdown mr-5 lang-bar language-bar contact log-reg-dash"
              >
                <a class="text-white" data-toggle="dropdown">
                  <i class="fa fa-language mobile-icon"></i>
                  <span class="drop-lang-caret">
                    {{ selectedLanguage.name }}
                    <i class="fa fa-caret-down drop-lang-caret"></i>
                  </span>
                </a>
                <div
                  :class="{ displayLangList: langDisplay }"
                  class="dropdown-menu dropdown-menu-left dropdown-menu-arrow lang-items"
                >
                  <a
                    v-for="(language, index) in languages"
                    :key="index"
                    class="dropdown-item "
                    @click="changeLang(language.urlCode)"
                  >
                    <i class="fa fa-globe pr-3 globe-mobile"></i>
                    {{ language.name }}
                  </a>
                </div>
              </li>
              <div
                class="dropdown-menu dropdown-menu-right dropdown-menu-arrow"
              >
                <langLink to="profile" class="dropdown-item">
                  <i class="dropdown-icon icon icon-user"></i> My Profile
                </langLink>
                <langLink to="/profile" class="dropdown-item">
                  <i class="dropdown-icon icon icon-speech"></i> Inbox
                </langLink>
                <langLink to="/profile" class="dropdown-item">
                  <i class="dropdown-icon icon icon-bell"></i>
                  Notifications
                </langLink>
                <langLink to="/register" class="dropdown-item">
                  <i class="dropdown-icon icon icon-power"></i> Log out
                </langLink>
              </div>
            </ul>
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
/* .displayLangList {
  display: block;
}
.header-whole {
  background-color: #0e1e25;
  position: fixed;
  height: 70px;
  top: 0;
  z-index: 1000;
  width: 100%;
}
.main-header {
  display: flex;
  justify-content: space-between;
  min-height: 51px;
}
.orange-button {
  background-color: #f37e1a;
}
.center-side-header {
  transform: translate(-50%, 10%);
}
.right-side-header {
  display: flex;
  color: #fff;
  width: auto;
  justify-content: space-between;
  transform: translate(0%, 25%);
}
.language-bar {
  display: flex;
  justify-content: flex-end;
}
.drop-lang-caret i {
  transform: translate(0%, 10%) !important;
}
.mobile-icon {
  display: none;
}
.language-bar:hover .lang-items {
  display: block;
}

.log-reg-dash {
  display: flex;
  cursor: pointer;
}
.log-reg-dash .text-white {
  display: flex;
  cursor: pointer;
}
.text-white i {
  transform: translate(-10%, 13%);
}
.log-reg-dash span:hover {
  color: #24abe3;
}
.log-out span {
  margin: 0 2px;
}
@media (max-width: 569px) and (min-width: 320px) {
  .main-hedaer {
    min-height: 60px;
    display: flex;
    justify-content: space-around;
  }
  .left-side-header {
    display: none;
  }
  .center-side-header img {
    text-align: center;
    float: right;
    transform: translate(50%, 0);
    padding-bottom: 10px;
  }

  .right-side-header i {
    font-size: 20px;
  }
  .lang-items {
    transform: translate(-60%, 10%);
  }
  .lang-items::after {
    display: none;
  }
  .globe-mobile i {
    display: none !important;
  }
  .mobile-icon {
    display: block;
    color: red;
    font-size: 20px;
  }
}
@media (max-width: 1280px) and (min-width: 992px) {
  .center-side-header {
    transform: translate(5%, 0);
  }
  .center-side-header img {
    width: 100px;
    margin-top: 10px;
  }
}
@media (max-width: 992px) and (min-width: 568px) {
  .only-web {
    display: flex;
    justify-content: space-between;
  }
  .main-header {
    text-align: center;
    width: 100%;
    display: flex;
    justify-content: center;
    min-height: 52px;
  }
  .center-side-header {
    margin: 0;
    padding: 0;
    padding-bottom: 10px;
  }
} */
</style>
