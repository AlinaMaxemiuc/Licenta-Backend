$(function () {
    var l = abp.localization.getResource('Renting');
    var createModal = new abp.ModalManager(abp.appPath + 'Drones/CreateModal');
    var editModal = new abp.ModalManager(abp.appPath + 'Drones/EditModal');

    var dataTable = $('#DronesTable').DataTable(
        abp.libs.datatables.normalizeConfiguration({
            serverSide: true,
            paging: true,
            order: [[1, "asc"]],
            searching: false,
            scrollX: true,
            ajax: abp.libs.datatables.createAjax(renting.drones.drone.getList),
            columnDefs: [
                {
                    title: l('Actions'),
                    rowAction: {
                        items: [
                            {
                                text: l('Edit'),
                                visible: abp.auth.isGranted('Renting.Drones.Edit'),
                                action: function (data) {
                                    editModal.open({ id: data.record.id });
                                }
                            },
                            {
                                text: l('Delete'),
                                visible: abp.auth.isGranted('Renting.Drones.Delete'),
                                confirmMessage: function (data) {
                                    return l('DroneDeletionConfirmationMessage', data.record.name);
                                },
                                action: function (data) {
                                    renting.drones.drone.delete(data.record.id).then(function () {
                                        abp.notify.info(l('SuccessfullyDeleted'));
                                        dataTable.ajax.reload();
                                    });
                                }
                            }
                        ]
                    }
                },
                { title: l('Name'), data: 'name' },
                { title: l('Model'), data: 'model' },
                {
                    title: l('Utility'),
                    data: "utility",
                    render: function (data) {
                        return l('Enum:DroneUtility.' + data);
                    }
                },
                {
                    title: l('Category'),
                    data: "category",
                    render: function (data) {
                        return l('Enum:DroneCategory.' + data);
                    }
                },
                {
                    title: l('ProductionYear'),
                    data: 'productionYear',
                    render: function (data) {
                        return luxon.DateTime.fromISO(data, { locale: abp.localization.currentCulture.name }).toLocaleString();
                    }
                },
                { title: l('PricePerDay'), data: 'pricePerDay' },
                {
                    title: l('CreationTime'),
                    data: 'creationTime',
                    render: function (data) {
                        return luxon.DateTime.fromISO(data, { locale: abp.localization.currentCulture.name }).toLocaleString(luxon.DateTime.DATETIME_SHORT);
                    }
                }
            ]
        })
    );

    createModal.onResult(function () {
        dataTable.ajax.reload();
    });

    editModal.onResult(function () {
        dataTable.ajax.reload();
    });

    $('#NewDroneButton').click(function (e) {
        e.preventDefault();
        createModal.open();
    });
});
