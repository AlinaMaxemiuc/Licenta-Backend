$(function () {
    var l = abp.localization.getResource('Renting');
    var createModal = new abp.ModalManager(abp.appPath + 'Reviews/CreateModal');
    var editModal = new abp.ModalManager(abp.appPath + 'Reviews/EditModal');

    var dataTable = $('#ReviewsTable').DataTable(
        abp.libs.datatables.normalizeConfiguration({
            serverSide: true,
            paging: true,
            order: [[1, "asc"]],
            searching: false,
            scrollX: true,
            ajax: abp.libs.datatables.createAjax(renting.reviews.review.getList),
            columnDefs: [
                {
                    title: l('Actions'),
                    rowAction: {
                        items: [
                            {
                                text: l('Edit'),
                                visible: abp.auth.isGranted('Renting.Reviews.Edit'),
                                action: function (data) {
                                    editModal.open({ id: data.record.id });
                                }
                            },
                            {
                                text: l('Delete'),
                                visible: abp.auth.isGranted('Renting.Reviews.Delete'),
                                confirmMessage: function (data) {
                                    return l('ReviewDeletionConfirmationMessage', data.record.name);
                                },
                                action: function (data) {
                                    renting.reviews.review.delete(data.record.id).then(function () {
                                        abp.notify.info(l('SuccessfullyDeleted'));
                                        dataTable.ajax.reload();
                                    });
                                }
                            }
                        ]
                    }
                },
                { title: l('Rating'), data: 'rating' },
                { title: l('Content'), data: 'content' },
                {
                    title: l('ReviewDate'),
                    data: 'reviewDate',
                    render: function (data) {
                        return luxon.DateTime.fromISO(data, { locale: abp.localization.currentCulture.name }).toLocaleString();
                    }
                },
                { title: l('DroneId'), data: 'droneId' },
                { title: l('CustomerId'), data: 'customerId ' },
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

    $('#NewReviewButton').click(function (e) {
        e.preventDefault();
        createModal.open();
    });
});
