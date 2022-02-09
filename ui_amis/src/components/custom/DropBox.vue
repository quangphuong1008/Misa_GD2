<template>
  <div class="dropbox">
    <div class="drop-box">
      <span>Thực hiện hàng loạt</span>
      <div
        class="icon-16 dropdown-black" :class="{'rotate': isShow}"
        v-on:click="isShow = !isShow"
      ></div>
    </div>
    <div class="drop-option" v-if="isShow" @click="multiDelete">
      <p>Xoá</p>
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
.dropbox{
  position: relative;

}
.drop-box {
  width: 180px;
  height: 36px;
  background-color: #ffffff;
  box-sizing: border-box;
  display: flex;
  align-items: center;
  border-radius: 20px;
  border: 2px solid #3b3c3f;
  cursor: pointer;
}
.drop-box:hover {
  background-color: #d2d3d6;
}
.drop-box span {
  padding: 4px 4px 4px 16px;
  font-family:  Notosans-Regular;
  font-size: 13px;
  text-align: center;
  font-weight: 600;
}
.drop-option {
  position: absolute;
      top: 37px;
    left: 69px;
  height: 34px;
  width: 104px;
  display: flex;
  align-items: center;
  background-color: #ffffff;
  border: 1px solid #babec5;
  z-index: 999;
  cursor: pointer;
}
.drop-option p {
  font-size: 13px;
  text-decoration: none solid rgb(17, 17, 17);
  text-align: left;
  padding: 5px 10px 5px 10px;
}
.drop-option:hover {
  background-color: #e8e9ec;
}
.icon-16 {
  height: 16px;
  width: 16px;
  min-width: 16px;
  min-height: 16px;
  
}
</style>