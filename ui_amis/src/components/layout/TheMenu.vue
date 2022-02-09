<template>
  <div class="m-menu">
    <div class="m-content__header">
      <div class="m-option"></div>
      <img src="../../assets/img/Logo.svg" class="logo" />
    </div>
    <div class="m-content__list-item">
      <router-link
        tag="div"
        class="m-menu-item"
        to="/home"
        active-class="selected"
      >
        <div class="m-menu-item__icon dashboard"></div>
        <div class="m-menu-item__tittle">Tổng quan</div>
      </router-link>

      <router-link
        tag="div"
        class="m-menu-item"
        to="/CAProcess"
        active-class="selected"
      >
        <div class="m-menu-item__icon cash"></div>
        <div class="m-menu-item__tittle">Tiền mặt</div>
      </router-link>

      <router-link
        tag="div"
        class="m-menu-item"
        to="/BAProcess"
        active-class="selected"
      >
        <div class="m-menu-item__icon bank"></div>
        <div class="m-menu-item__tittle">Tiền gửi</div>
      </router-link>

      <router-link
        tag="div"
        class="m-menu-item"
        to="/PUProcess"
        active-class="selected"
      >
        <div class="m-menu-item__icon shopping"></div>
        <div class="m-menu-item__tittle">Mua hàng</div>
      </router-link>

      <router-link
        tag="div"
        class="m-menu-item"
        to="/SAProcess"
        active-class="selected"
      >
        <div class="m-menu-item__icon sale"></div>
        <div class="m-menu-item__tittle">Bán hàng</div>
      </router-link>

      <router-link
        tag="div"
        class="m-menu-item"
        to="/IPProcess"
        active-class="selected"
      >
        <div class="m-menu-item__icon invoice"></div>
        <div class="m-menu-item__tittle">Quản lý hóa đơn</div>
      </router-link>

      <router-link
        tag="div"
        class="m-menu-item"
        to="/INProcess"
        active-class="selected"
      >
        <div class="m-menu-item__icon stock"></div>
        <div class="m-menu-item__tittle">Kho</div>
      </router-link>

      <router-link
        tag="div"
        class="m-menu-item"
        to="/SUProcess"
        active-class="selected"
      >
        <div class="m-menu-item__icon tool"></div>
        <div class="m-menu-item__tittle">Công cụ dụng cụ</div>
      </router-link>

      <router-link
        tag="div"
        class="m-menu-item"
        to="/FAProcess"
        active-class="selected"
      >
        <div class="m-menu-item__icon fixed-asset"></div>
        <div class="m-menu-item__tittle">Tài sản cố định</div>
      </router-link>

      <router-link
        tag="div"
        class="m-menu-item"
        to="/TAList"
        active-class="selected"
      >
        <div class="m-menu-item__icon tax"></div>
        <div class="m-menu-item__tittle">Thuế</div>
      </router-link>

      <router-link
        tag="div"
        class="m-menu-item"
        to="/JCProcess"
        active-class="selected"
      >
        <div class="m-menu-item__icon price"></div>
        <div class="m-menu-item__tittle">Giá thành</div>
      </router-link>

      <router-link
        tag="div"
        class="m-menu-item"
        to="/GLProcess"
        active-class="selected"
      >
        <div class="m-menu-item__icon general"></div>
        <div class="m-menu-item__tittle">Tổng hợp</div>
      </router-link>

      <router-link
        tag="div"
        class="m-menu-item"
        to="/BUDDashboard"
        active-class="selected"
      >
        <div class="m-menu-item__icon budget"></div>
        <div class="m-menu-item__tittle">Ngân sách</div>
      </router-link>

      <router-link
        tag="div"
        class="m-menu-item"
        to="/ReportList"
        active-class="selected"
      >
        <div class="m-menu-item__icon report"></div>
        <div class="m-menu-item__tittle">Báo cáo</div>
      </router-link>

      <router-link
        tag="div"
        class="m-menu-item"
        to="/AnalysisIndex"
        active-class="selected"
      >
        <div class="m-menu-item__icon finance"></div>
        <div class="m-menu-item__tittle">Phân tích tài chính</div>
      </router-link>
    </div>
  </div>
</template>

<script>

export default {};

</script>

<style scope>
@import url("../../assets/styles/base/icon.css");

.m-menu-item {
  display: flex;
  padding-right: 14px;
  padding-left: 8px;
  align-items: center;
  height: 42px;
  border-left: 6px solid transparent;
  cursor: pointer;
}

.m-menu-item:hover {
  background: rgba(235, 237, 240, 0.2);
}

.m-menu-item .m-menu-item__icon {
  width: 24px;
  height: 24px;
  margin-right: 8px;
}

.m-menu-item .m-menu-item__tittle {
  font-family: Notosans-Regular;
  font-size: 13px;
  color: #ffffff;
}

.m-menu-item.selected {
  border-color: #2ca01c;
  background: rgba(235, 237, 240, 0.2);
  font-weight: 700;
}
.m-menu-item.selected .m-menu-item__icon {
  background-position-y: -1585px;
}

.m-menu {
  background-color: #393a3d;
  overflow: hidden;
  min-width: 200px;
}

.m-menu .m-option {
  width: 24px;
  height: 24px;
  background: url("../../assets/img/Sprites.svg") no-repeat -424px -86px;
  margin-right: 10px;
}

.m-menu .logo {
  height: 32px;
} 

.m-menu .m-content__header {
  display: flex;
  padding: 10px;
  align-items: center;
  justify-content: center;
  height: 48px;
  box-sizing: border-box;
}

.m-menu .m-content__list-item{
  padding-top: 14px;
  height: calc(100vh - 48px);
  overflow: auto;
  scrollbar-width: thin;
}
</style>