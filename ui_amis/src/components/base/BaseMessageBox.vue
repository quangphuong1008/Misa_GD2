<template>
  <div class="m-message-box">
    <div class="m-message-box__main" :style="{ width: width }">
      <div class="m-message-box__main--body">
        <div :class="['m-msg-icon', Icon()]"></div>
        <div class="m-msg-text">{{ text }}</div>
      </div>
      <div class="mess-line"></div>
      <div v-if="type == 'warning'" class="m-message-box__main--footer">
        <div class="m-message-box-btn" @click="handleCancle">Không</div>
        <div class="m-message-box-btn btn-primary" @click="handleConfirm">
          Có
        </div>
      </div>
      <div v-else-if="type == 'error'" class="m-message-box__main--footer" style="justify-content:center">
        <div class="m-message-box-btn btn-primary" @click="handleCancle">Đóng</div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  props: {
    width: String,
    text: String,
    type: String,
  },
  methods: {
    Icon() {
      if (this.type === "warning") {
        return "warning";
      } else if (this.type === "error") {
        return "error";
      }
    },
    handleConfirm() {
      this.$emit("handleConfirmMessage");
    },
    handleCancle() {
      this.$emit("handleCanclemMessage");
    },
  },
};
</script>

<style scoped>
.m-message-box-btn {
  padding: 6px 16px;
  border: 1px solid #8d9096;
  border-radius: 3px;
  cursor: pointer;
  color: #111;
  font-size: 13px;
  font-weight: 600;
}
.m-message-box-btn:hover {
  opacity: 0.5;
}
.btn-primary {
  background-color: #2ca01c;
  border: 1px solid transparent;
  color: #fff;
}
.m-message-box__main--footer {
  display: flex;
  justify-content: space-between;
}
.mess-line {
  height: 1px;
  margin-bottom: 20px;
  background: #b8bcc3;
}
.m-message-box {
  position: fixed;
  background: rgba(0, 0, 0, 0.4);
  top: 0;
  left: 0;
  right: 0;
  bottom: 0;
  z-index: 999;
  display: flex;
  justify-content: center;
  align-items: center;
  animation: fadein 0.5s;
}
.m-message-box .m-message-box__main {
  padding: 32px;
  background-color: #fff;
  border-radius: 5px;
  box-sizing: border-box;
  animation: scale 0.5s;
}
.m-message-box .m-message-box__main .m-message-box__main--body {
  display: flex;
}
.m-message-box .m-message-box__main .m-message-box__main--body .m-msg-text {
  padding-left: 16px;
  padding-top: 12px;
  margin-bottom: 32px;
}
.m-msg-icon {
  width: 48px;
  height: 48px;
}
.warning {
  background: url("../../assets/img/Sprites.svg") no-repeat;
  background-position: -592px -456px;
}
.error {
  background: url("../../assets/img/Sprites.svg") no-repeat;
  background-position: -24px -954px;
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