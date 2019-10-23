<template>
  <div class="card">
    <div class="card-body  item-user">
      <div class="profile-pic mb-0">
        <img
          src="https://i2.au.reastatic.net/120x160/e390fbbdaf1679acaf0323ababafe840cf63315b26ebe48c17af8cefed0b938f/main.jpg"
          class="brround avatar-xxl"
          alt="user"
        />
        <div class="">
          <a href="userprofile2.html" class="text-dark"
            ><h4 class="mt-3 mb-1 font-weight-semibold">
              {{ agent.agent.firstName }} {{ agent.agent.lastName }}
            </h4></a
          >
          <p class="mb-0">RealEstate Agent</p>
          <span class="text-muted">{{ agent.agent.email }}</span>
          <h6 class="mt-2 mb-0">
            <a href="userprofile2.html" class="btn btn-primary btn-sm"
              >See All Ads</a
            >
          </h6>
        </div>
      </div>
    </div>
  </div>
</template>
<script>
export default {
  name: 'SingleAgent',
  props: ['agent'],
  data() {
    return {
      agentList: []
    }
  }
}
</script>
<style scoped></style>
