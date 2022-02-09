<template>
  <div class="btn__">
    <div class="btn-add">
      <button class="add" @click="handleBtnAdd">Thêm</button>
      <button
        class="opiton"
        :class="{ rotate: isShow }"
        v-on:click="isShow = !isShow"
      >
        <div class="icon-16 dropdown-while"></div>
      </button>

      <!-- <button @click="handleBtnAdd" style="border-right: 1px solid red">
        Thêm
      </button>
      <button
        class="icon-16 dropdown-black"
        :class="{ rotate: isShow }"
        style="width: 46px; padding: 8px 10px"
        v-on:click="isShow = !isShow"
      ></button> -->
    </div>
    <div class="multi-option" v-if="isShow" @click="inputExcel">
      <p>Nhập từ Excel</p>
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
    inputExcel() {
      this.$emit("inputExcel");
      this.isShow = false;
    },
    handleBtnAdd() {
      this.$emit("handleBtnAdd");
    },
  },
};
</script>

<style>
.rotate {
  transform: rotateX(3.142rad);
}
.btn__ {
  position: relative;
}
.btn-add {
  width: 116px;
  height: 36px;
  background-color: #2ca01c;
  box-sizing: border-box;
  display: flex;
  align-items: center;
  border-radius: 20px;
  cursor: pointer;
}
.btn__ .btn-add .add {
  height: 100%;
  width: 70px;
  border-radius: 20px 0 0 20px;
  padding: 10px 16px 10px 20px;
  border-right: 1px solid #ffffff;
  background-color: inherit;  
  color: #ffffff;
}
.btn__ .btn-add .opiton {
  height: 100%;
  width: 46px;
  padding: 8px 10px;
  border-radius: 0 20px 20px 0;
  background-color: inherit;

}
.btn__ .btn-add button:hover {
  background-color: #11da2c;
}
.btn-add button {
  font-family: Notosans-Regular;
  font-size: 13px;
  text-align: center;
  font-weight: 600;
  display: flex;
  align-items: center;
  border: none;
}
.multi-option {
  position: absolute;
  top: 38px;
  left: 10px;
  height: 34px;
  width: 108px;
  display: flex;
  align-items: center;
  background-color: #ffffff;
  border: 1px solid #babec5;
  z-index: 999;
  cursor: pointer;
}
.multi-option p {
  font-size: 13px;
  text-decoration: none solid rgb(17, 17, 17);
  text-align: left;
  padding: 5px 10px 5px 10px;
}
.multi-option:hover {
  background-color: #e8e9ec;
}
.icon-16 {
  height: 16px;
  width: 16px;
  min-width: 16px;
  min-height: 16px;
}
</style>