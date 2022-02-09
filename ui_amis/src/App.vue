<template>
  <div id="app">
    <Menu />
    <div id="wrapper">
      <Header />
      <Content>
        <router-view/>
      </Content>
    </div>
  </div>
</template>

<script>
import Content from "./components/layout/TheContent.vue";
import Menu from "./components/layout/TheMenu.vue";
import Header from "./components/layout/TheHeader.vue";

export default {
  components: { Menu, Content, Header },
  created() {
    document.title = "AMIS";
  },
};
</script>

<style>

@import url("./assets/styles/base/normalize.css");
@import url("./assets/styles/base/base.css");
#app {
  display: flex;
  
}
#app #wrapper{
  flex: 1;
  display: flex;
  flex-direction: column;
}
</style>