<template>
  <div class="m-cash" style="position: relative">
    <!--Phần đầu trang-->
    <ul class="m-cash-header" style="margin: 0">
      <li class="header-content" @click="isShow = 0" :class="isShow == 0 ? 'm-process' : ''">
        <p>Quy trình</p>
      </li>

      <!-- <router-link to="/CAProcess/CAPayment" style="text-decoration: none"
        ><li class="header-content"><p>Chi tiền</p></li></router-link
      > -->
      <li class="header-content" @click="isShow = 1" :class="isShow == 1 ? 'm-process' : ''">
        <p>Chi tiền</p>
      </li>
      <li class="header-content">
        <p>Kiểm kê</p>
      </li>
      <li class="header-content">
        <p>Dòng tiền</p>
      </li>
      <li class="header-content">
        <p>Báo cáo</p>
      </li>
    </ul>

    <!--Phần nội dung trang -->
    <div>
      <div v-show="isShow == 0" style="display: flex">
        <div
          style="
            width: 100%;
            height: calc(100% - 166px);
            display: flex;
            align-items: center;
            justify-content: center;
            position: absolute;
            top: 123px;
          "
        >
          <div class="m-cash-model">
            <div class="m-model-title">
              <p>NGHIỆP VỤ TIỀN MẶT</p>
            </div>
            <div class="m-model-content">
              <div class="m-major m-receive"><p>Thu tiền</p></div>
              <div class="m-major m-pay"><p>Chi tiền</p></div>
              <div class="m-major m-inventory"><p>Kiểm kê quỹ</p></div>
            </div>
            <div class="m-model-item">
              <div class="m-item-detail">
                <div class="detail">
                  <div class="m-icon icon-24 m-customer"></div>
                  <p>Khách hàng</p>
                </div>
              </div>
              <router-link
                to="/CAProcess/accountObjs"
                style="text-decoration: none"
              >
                <div class="m-item-detail">
                  <div class="detail">
                    <div class="m-icon icon-24 m-provider"></div>
                    <p>Nhà cung cấp</p>
                  </div>
                </div>
              </router-link>
              <router-link
                to="/CAProcess/employees"
                style="text-decoration: none"
              >
                <div class="m-item-detail">
                  <div class="detail">
                    <div class="m-icon icon-24 m-employee"></div>
                    <p>Nhân viên</p>
                  </div>
                </div>
              </router-link>
              <div class="m-item-detail">
                <div class="detail">
                  <div class="m-icon icon-24 m-account"></div>
                  <p>Tài khoản</p>
                </div>
              </div>
            </div>
          </div>
          <div class="m-cash-report">
            <div class="m-model-title">
              <p>BÁO CÁO</p>
            </div>
            <div class="m-report-content">
              <ul>
                <li>Dòng tiền</li>
                <li>Bảng kê số dư tiền theo ngày</li>
                <li>Sổ kế toán chi tiết quỹ tiền mặt</li>
                <li>S03a2-DN: Sổ nhật ký chi tiền</li>
                <li id="m-last-report">S03a1-DN: Sổ nhật ký thu tiền</li>
              </ul>
            </div>
            <div class="m-report-footer">
              <p>Tất cả báo cáo</p>
            </div>
          </div>
        </div>
      </div>

      <CAPayment v-if="isShow == 1" />
    </div>
  </div>
</template>

<script>
import CAPayment from "./ca_payment/CAPayment.vue";
export default {
  components: { CAPayment },
  data() {
    return {
      isShow: 0,
    };
  },
};
</script>

<style scoped>
.m-cash {
  background-color: #f4f5f8;
  height: calc(100vh - 55px);
  font-size: 13px;
  margin: 0;
  color: #111;
}
.m-cash-header {
  display: flex;
  padding: 14px 0px 0px 14px;
  border-bottom: 1px solid #b8bcc3;
}
.header-content {
  display: flex;
  align-items: center;
  justify-content: space-around;
  width: 110px;
  height: 45px;
  cursor: pointer;
}
.m-process {
  font-weight: bold;
  border-bottom: 4px solid #2ca01c;
}

.m-cash-model {
  background-color: #fff;
  height: 460px;
  width: 690px;
  border: 1px solid #e5e5e5;
  border-radius: 3px;
}
.m-model-title {
  text-transform: uppercase;
  height: 40px;
  display: flex;
  justify-content: center;
  align-items: center;
  font-size: 14px;
  font-weight: 700;
  border-bottom: 1px solid #e5e5e5;
}
.m-model-content {
  position: relative;
  background-image: url("../assets/img/qt_TienMat.1f64b87b.svg");
  height: 349px;
  width: 690px;
  padding-top: 30px;
}
.m-major {
  width: 80px;
  height: 90px;
  display: flex;
  align-items: flex-end;
  justify-content: center;
}
.m-major:hover {
  cursor: pointer;
  box-shadow: 4px 4px #babec5;
  box-shadow: 0 3px 6px rgb(0 0 0 / 16%);
}
.m-receive {
  position: absolute;
  top: 47px;
  left: 95px;
}
.m-pay {
  position: absolute;
  top: 220px;
  left: 95px;
}
.m-inventory {
  position: absolute;
  top: 135px;
  left: 355px;
}

.m-model-item {
  display: flex;
  justify-content: space-between;

  background-color: #fff;
  height: 70px;
  width: 690px;
  border-top: 1px solid #e5e5e5;
  border-bottom: 1px solid #e5e5e5;
  border-left: 1px solid #e5e5e5;
  border-radius: 3px;
}
.m-item-detail {
  height: 70px;
  width: 170px;
  border-right: 1px solid #e5e5e5;
  display: flex;
  align-items: center;
  justify-content: center;
}
.m-item-detail:hover {
  cursor: pointer;
  box-shadow: 4px 4px #babec5;
  box-shadow: 0 3px 6px rgb(0 0 0 / 16%);
}
.detail {
  padding-top: 12px;
  display: flex;
  flex-direction: column;
  align-items: center;
}
.m-icon {
  background-image: url("../assets/img/Sprites.64af8f61.svg");
  background-repeat: no-repeat;
  cursor: pointer;
}
.icon-24 {
  height: 24px;
  width: 24px;
  min-width: 24px;
  min-height: 24px;
}
.m-customer {
  background-position: -872px -200px;
}
.m-provider {
  background-position: -928px -200px;
}
.m-employee {
  background-position: -984px -200px;
}
.m-account {
  background-position: -1096px -200px;
}
.m-cash-report {
  background-color: #fff;
  height: 460px;
  width: 300px;
  border: 1px solid #e5e5e5;
  border-radius: 3px;
}
.m-report-content {
  width: 300px;
  height: 378px;
}
.m-report-content ul {
  margin: 0;
  padding: 0 16px;
  list-style-type: disc;
}
.m-report-content ul li {
  display: flex;
  align-items: center;
  border-bottom: 1px solid #e5e5e5;
  height: 76px;
}
.m-report-content ul li::before {
  content: "\2022";
  color: #646464;
  font-size: 24px;
  margin-right: 12px;
}
.m-report-content ul li:hover {
  cursor: pointer;
  color: #0075c0;
}
.m-report-footer {
  width: 300px;
  height: 40px;
  border-top: 1px solid #e5e5e5;
  cursor: pointer;
  height: 44px;
  display: flex;
  justify-content: center;
  align-items: center;
  font-size: 13px;
  color: #0075c0;
}
#m-last-report {
  border-bottom: none;
}
</style>