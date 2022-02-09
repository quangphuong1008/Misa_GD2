<template>
  <div class="btn__">
    <div class="btn-add" style="width:160px">
      <button class="add" @click="handleBtnAdd"
      style="font-size:12px; width:115px">Thêm chi tiền</button>
      <button
        class="opiton"
        :class="{ rotate: isShow }"
        v-on:click="isShow = !isShow"
      >
        <div class="icon-16 dropdown-while"></div>
      </button>
    </div>
    <div class="m-option-dropdown__body" v-if="isShow" @click="inputExcel">
      <div class="m-option-dropdown__item">Phiếu chi</div>
      <div class="m-option-dropdown__item" >Trả tiền theo hóa đơn</div>
      <div class="m-option-dropdown__item">Nộp thuế</div>
      <div class="m-option-dropdown__item">Nhập theo Excel</div>
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