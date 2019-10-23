<template>
  <div class="bottom-header">
    <div class="horizontal-header clearfix ">
      <div class="container">
        <a id="horizontal-navtoggle" class="animated-arrow"><span></span></a>
        <!-- <langLink id="horizontal-navtoggle" class="animated-arrow" to="/"
          ><img src="~/static/images/logo.png" width="120" alt=""
        /></langLink> -->
        <!-- <span><span></span></a> -->
        <span class="smllogo"
          ><langLink to="/"
            ><img src="~/static/images/logo2.png" width="120" alt=""/></langLink
        ></span>
        <a href="tel:245-6325-3256" class="callusbtn"
          ><i class="fa fa-phone" aria-hidden="true"></i
        ></a>
      </div>
    </div>
    <!-- the menues -->
    <div class="horizontal-main bg-dark-transparent clearfix">
      <div class="horizontal-mainwrapper container clearfix">
        <div class="desktoplogo">
          <langLink to="/"><img src="~/static/images/logo4.png"/></langLink>
        </div>
        <!-- <div class="desktoplogo-1">
          <a href="index.html"><img src="~/static/images/logo3.png" alt=""/></a>
        </div> -->
        <!--TODO: Nav-->
        <nav class="horizontalMenu clearfix d-md-flex mt-2">
          <ul class="horizontalMenu-list">
            <li aria-haspopup="true">
              <nuxt-link
                class="active btn btn-primary top-header-li"
                :to="
                  $route.params.lang ? `/${$route.params.lang}/Home` : '/Home'
                "
                >Home</nuxt-link
              >
              <!-- <LangLink class="active btn btn-secondary" to="/">
                <span>Home</span>
              </LangLink> -->
            </li>
            <li aria-haspopup="true">
              <nuxt-link
                :to="
                  $route.params.lang ? `/${$route.params.lang}/About` : '/About'
                "
                class="btn top-header-li"
                >About</nuxt-link
              >
              <!-- <LangLink to="/About" class="btn btn-secondary">
                <span>About</span>
              </LangLink> -->
            </li>
            <li aria-haspopup="true">
              <nuxt-link
                :to="
                  $route.params.lang
                    ? `/${$route.params.lang}/Contact-us`
                    : '/Contact-us'
                "
                class="btn top-header-li"
                >Contact Us</nuxt-link
              >
              <!-- <LangLink to="/Contact-us" class="btn btn-secondary">
                <span>Contact Us</span>
              </LangLink> -->
            </li>
            <li aria-haspopup="true">
              <nuxt-link
                :to="
                  $route.params.lang
                    ? `/${$route.params.lang}/Agents`
                    : '/Agents'
                "
                class="btn top-header-li"
                >Agents</nuxt-link
              >
              <!-- <LangLink to="/Agents" class="btn btn-secondary">
                <span>Agents</span>
              </LangLink> -->
            </li>
            <li aria-haspopup="true" class="d-lg mt-lg-0">
              <nuxt-link
                :to="
                  $route.params.lang
                    ? `/${$route.params.lang}/submit-property`
                    : '/submit-property'
                "
                class="active btn btn-secondary"
                >Submit Property</nuxt-link
              >
              <!-- <a class="btn btn-secondary" href="/">Submit Property</a> -->
            </li>
            <!-- <li aria-haspopup="true" class="mt-3 d-lg-block top-postbtn">
              <span
                ><a class="btn btn-secondary ad-post " href="ad-posts.html"
                  >Post Property Ad</a
                ></span
              >
            </li> -->
          </ul>
          <!-- <ul class="mb-0">
            <li aria-haspopup="true" class="mt-3 d-none d-lg-block top-postbtn">
              <span
                ><a class="btn btn-secondary ad-post " href="ad-posts.html"
                  >Post Property Ad</a
                ></span
              >
            </li>
          </ul> -->
        </nav>
        <!--Nav-->
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: 'BottomHeader',
  data() {
    return {
      generatedLink: ''
    }
  },
  created() {}
}
</script>

<style>
div.bottom-header {
  position: -webkit-sticky;
  position: sticky;
  top: 0;
}
.top-header-li:hover {
  background-color: #24abe3 !important;
}
.horizontalMenu > .horizontalMenu-list > li > a {
  display: block;
  padding: 3px 15px;
  text-decoration: none;
  position: relative;
  margin: 0 2px;
  border-radius: 3px;
}
.desktoplogo {
  margin-left: 20px !important;
}
</style>
