<template>
  <div class="m-popup">
    <div class="m-popup-content" :style="{ width: width }">
      <div class="m-popup__header">
        <div class="m-popup-tittle">
          Thông tin nhân viên
          <Checkbox class="custom-padding" :label="'Là khách hàng'" />
          <Checkbox class="custom-padding" :label="'Là nhà cung cấp'" />
        </div>
        <div class="m-popup-close">
          <div class="m-popup-btn help" @click="handleHelpPopup"></div>
          <div class="m-popup-btn close" @click="handleClosePopup"></div>
        </div>
      </div>
      <div class="m-popup-body">
        <slot name="body"></slot>
        <div class="m-popup-footer">
          <div class="divide"></div>
          <slot name="footer"></slot>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import Checkbox from "../base/BaseCheckbox.vue";
export default {
  components: {
    Checkbox,
  },
  props: {
    width: String
  },
  methods: {
    /**
     * Hàm bắn ra emit cho Component cha
     * Author: nmquang (2021/12/24)
     */
    handleClosePopup() {
      this.$emit("handleClosePopup");
    },
    /**
     *  Hàm khi ấn nút Help
     *  Author: nmquang(2021/12/24)
     */
    handleHelpPopup() {
      this.$emit("handleHelpPopup");
    },
  },
  mounted(){
    this.$parent.$refs.EmployeeCode.$el.querySelector("input").focus();
  }
};
</script>

<style scope>
.m-popup-footer .divide{
  margin: 32px 0 20px 0;
  border-top: 1px solid #e0e0e0;
}

.custom-padding {
  padding-left: 19.5px;
  padding-right: 19.5px;
}
.m-popup-tittle {
  display: flex;
  align-items: center;
}
.m-popup-close {
  display: flex;
}
.m-popup-close .m-popup-btn {
  cursor: pointer;
  height: 24px;
  width: 24px;
}
.m-popup-close .m-popup-btn + .m-popup-btn {
  margin-left: 6px;
}
.m-popup-content {
  background-color: #fff;
  border-radius: 5px;
  animation: scale 0.5s;
}

.m-popup__header {
  display: flex;
  justify-content: space-between;
}
.m-popup__header .m-popup-tittle {
  padding: 20px 12px 20px 32px;
  font-size: 24px;
  color: #111;
  font-weight: 700;
  font-family: Notosans-Regular;
}
.m-popup__header .m-popup-close {
  padding: 12px;
}
.m-popup {
  position: fixed;
  top: 0;
  left: 0;
  right: 0;
  bottom: 0;
  background-color: rgba(0, 0, 0, 0.4);
  display: flex;
  justify-content: center;
  align-items: center;
  animation: fadein 0.5s;
  z-index: 999;
}

@keyframes fadein {
  from {
    opacity: 0;
  }
  to {
    opacity: 1;
  }
}
@keyframes scale {
  from {
    transform: scale(0.7);
  }
  to {
    transform: scale(1);
  }
}
</style>