<template>
  <div class="m-header">
    <div class="m-header__left">
      <div class="m-header__icon menu"></div>
      <div class="m-header__tittle">
        <div class="branch-name">
          CÔNG TY TNHH SẢN XUẤT - THƯƠNG MẠI - DỊCH VỤ QUI PHÚC
        </div>
        <div class="tittle-option arrow"></div>
      </div>
    </div>
    <div class="m-header__right">
      <div class="m-header__notify bell"></div>
      <div class="user">
        <div class="avatar"></div>
        <div class="username">Nguyễn Mạnh Quang</div>
        <div class="user-option arrow"></div>
      </div>
    </div>
  </div>
</template>

<script>
export default {};
</script>

<style scope>
.m-header {
  height: 48px;
  display: flex;
  align-items: center;
  justify-content: space-between;
}

.m-header__tittle {
  display: flex;
  align-items: center;
}

.m-header__tittle .branch-name {
  padding-left: 10px;
  font-family: Notosans-Regular;
  font-size: 14px;
  color: #111;
  font-weight: 600;
  white-space: nowrap;
  overflow: hidden;
  text-overflow: ellipsis;
}
.m-header__tittle .tittle-option {
  width: 14px;
  height: 14px;
  transform: rotate(90deg);
  padding: 10px 0px;
  box-sizing: content-box;
}
.m-header__tittle:hover {
  cursor: pointer;
}
.m-header__icon {
  width: 24px;
  height: 24px;
  margin: 0 10px;
}
.m-header__notify {
  width: 24px;
  height: 24px;
}
.m-header__left {
  display: flex;
  align-items: center;
}
.m-header__right {
  display: flex;
  align-items: center;
  padding-right: 20px;
}
.m-header__right .user{
    display: flex;
    align-items: center;
    padding: 0 16px;
}
.m-header__right .user .avatar {
  background: url("../../assets/img/Sprites.svg") no-repeat -352px -894px;
  width: 32px;
  height: 32px;
  border-radius: 50%;
  margin-right: 6px;
}
.m-header__right .user .username {
  font-weight: 600;
  font-display: Notosans-Regular;
  font-size: 14px;
  margin-right: 8px;
}
.m-header__right .user .user-option{
    width: 14px;
    height: 14px;
    transform: rotate(90deg);
}
</style>