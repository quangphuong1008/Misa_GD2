<template>
  <div class="droptool">
    <div class="drop-box">
      <span>Tiện ích</span>
      <div
        class="icon-16 dropdown-black" :class="{'rotate': isShow}"
        v-on:click="isShow = !isShow"
      ></div>
    </div>
    <div class="drop-option" v-if="isShow" @click="multiDelete">
      <p>Gộp nhà cung cấp tự động</p>
    </div>
  </div>
</template>

<script>
export default {
  data() {
    return {
      isShow: false,
    };
  },
  methods: {

    /**
     * Thuc hien xoa
     */
    multiDelete(){
      this.$emit('multiDelete')
      this.isShow = false
    }
  },
  
};
</script>

<style>
.rotate{
  transform: rotateX(3.142rad);
}
.droptool{
  position: relative;
}
.droptool .drop-box{
  width: 105px;
  margin: 0 12px;
}
.droptool .drop-option{
  left: -73px;
    height: 34px;
    width: 186px;
}


</style>