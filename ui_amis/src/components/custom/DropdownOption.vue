<template>
  <div
    :class="['m-option-dropdown-btn', { border: selected }]"
    @click="
      isShow = !isShow;
      selected = !selected;
    "
  >
    <div style="width: 16px; height: 16px" class="dropdown-blue"></div>
    <div class="m-option-dropdown__body" v-if="isShow">
      <div class="m-option-dropdown__item" @click="Clone">Nhân bản</div>
      <div class="m-option-dropdown__item" @click="Delete">Xóa</div>
      <div class="m-option-dropdown__item">Ngừng sử dụng</div>
    </div>
  </div>
</template>

<script>
export default {
  data() {
    return {
      selected: false,
      isShow: false,
    };
  },
  methods:{
      /**
       * Hàm bắn ra sự kiện khi ấn nút xóa
       * @Author nmquang 19-12-2021
       */
      Delete(){
          this.$emit('delete');
      },
      /**
       * Hàm bắn ra sự kiện khi ấn nút nhân bản
       * @Author nmquang 19-12-2021
       */
      Clone(){
        this.$emit('clone')
      },
  }
};
</script>

<style scope>
.m-option-dropdown__item{
    padding: 5px 10px;
}
.m-option-dropdown__item:hover{
    background-color: #e8e9ec;
    color: #08bf1e;
}
.m-option-dropdown-btn {
  position: relative;
  overflow: unset;
  width: 26px;
  height: 20px;
  margin: 0 10px;
  display: flex;
  justify-content: center;
  align-items: center;
  cursor: pointer;
}
.m-option-dropdown__body {
  white-space: nowrap;
  position: absolute;
  top: calc(100% + 2px);
  right: 0;
  background-color: #fff;
  z-index: 3;

  padding: 2px 1px;
  border-radius: 2px;
  border: 1px solid #babec5;

  -webkit-user-select: none;
  -khtml-user-select: none;
  -moz-user-select: none;
  -ms-user-select: none;
  -o-user-select: none;
  user-select: none;
}
.border {
  border: 1px solid #0075c0;
}
</style>