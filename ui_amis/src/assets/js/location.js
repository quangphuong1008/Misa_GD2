


const Location = {
    VN: {
        countries :[
            {
                location_id: "1",
                location_name: 'Việt Nam'
            },
            {
                location_id: "2",
                location_name: 'Mỹ'
            }
        ],

        cities :[
            {
                location_id: "3",
                location_name: 'Hà Nội',
                parent_id: "1"
            },
            {
                location_id: "4",
                location_name: 'Thái Bình',
                parent_id: "1"
            },
            {
                location_id: "111",
                location_name: 'TP Hồ Chí Minh',
                parent_id: "1"
            },
            {
                location_id: "112",
                location_name: 'Quảng Ninh',
                parent_id: "1"
            },{
                location_id: "113",
                location_name: 'Thừa Thiên Huế',
                parent_id: "1"
            },
            {
                location_id: "114",
                location_name: 'Đà Nẵng',
                parent_id: "1"
            },
            {
                location_id: "115",
                location_name: 'Hải Phòng',
                parent_id: "1"
            }
        ],
        district :[
            {
                location_id: "5",
                location_name: 'Ba Đình',
                parent_id: "3"
            },
            {
                location_id: "6",
                location_name: 'Cầu Giấy',
                parent_id: "3"
            },
            {
                location_id: "221",
                location_name: 'Phú Diễn',
                parent_id: "3"
            },
            {
                location_id: "222",
                location_name: 'Tây Hồ',
                parent_id: "3"
            },
            {
                location_id: "223",
                location_name: 'Hoàn Kiếm',
                parent_id: "3"
            },
            {
                location_id: "224",
                location_name: 'Đông Anh',
                parent_id: "3"
            },
            {
                location_id: "7",
                location_name: 'Kiến Xương',
                parent_id: "4"
            },
            {
                location_id: "8",
                location_name: 'Tiền Hải',
                parent_id: "4"
            }
        ],
        wardOrCommune :[
            {
                location_id: "9",
                location_name: 'Phường đội cấn',
                parent_id: "5"
            },
            {
                location_id: "10",
                location_name: 'Phường Cống Vị',
                parent_id: "5"
            },
            {
                location_id: "10",
                location_name: 'Phường Dịch Vọng',
                parent_id: "6"
            },
            {
                location_id: "12",
                location_name: 'Phường Quan Hoa',
                parent_id: "6"
            },
            {
                location_id: "13",
                location_name: 'Xã Minh Quang',
                parent_id: "7"
            },
            {
                location_id: "14",
                location_name: 'Xã Minh Tân',
                parent_id: "7"
            },
            {
                location_id: "15",
                location_name: 'Xã Bình Định',
                parent_id: "8"
            },
            {
                location_id: "16",
                location_name: 'Xã Bình Thanh',
                parent_id: "8"
            }
        ]
    }
}


export default Location;