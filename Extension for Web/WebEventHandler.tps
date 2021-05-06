import { CustomButton } from "@docsvision/webclient/Platform/CustomButton";
import { DateTimePicker } from "@docsvision/webclient/Platform/DateTimePicker";
import { NumberControl } from "@docsvision/webclient/Platform/Number";
import { TextArea } from "@docsvision/webclient/Platform/TextArea";
import { IEventArgs } from "@docsvision/webclient/System/IEventArgs";
import { Layout } from "@docsvision/webclient/System/Layout";
import { layoutManager } from "@docsvision/webclient/System/LayoutManager";
import { func } from "prop-types";
import { CancelableEventArgs } from "@docsvision/webclient/System/CancelableEventArgs";
import { ICardSavingEventArgs } from "@docsvision/webclient/System/ICardSavingEventArgs";
import { MessageBox } from "@docsvision/webclient/Helpers/MessageBox/MessageBox";
import { TextBox } from "@docsvision/webclient/Platform/TextBox";
import { Employee } from "@docsvision/webclient/BackOffice/Employee";
import { Numerator } from "@docsvision/webclient/BackOffice/Numerator";
import { $OneController } from "../Controllers/OneController";
import { MultipleEmployees } from "@docsvision/webclient/BackOffice/MultipleEmployees";
import { DirectoryDesignerRow } from "@docsvision/webclient/BackOffice/DirectoryDesignerRow";
import { State } from "@docsvision/webclient/BackOffice/State";
import { CardInfoModel } from "@docsvision/webclient/Legacy/CardInfoModel";

//Some handlers
export async function sampleEventHandler(sender: Layout, e: IEventArgs): JQueryDeferred<void> {
  //some code
}

export async function CountDaysComand(sender: DateTimePicker, e: any): JQueryDeferred<void> {  //Количество дней 

    let layout = sender.layout;
    let Date1 = layout.controls.tryGet<DateTimePicker>("DataKomandirovkiOt");
    let Date2 = layout.controls.tryGet<DateTimePicker>("DataPo");
    let CountDays = layout.controls.tryGet<NumberControl>("KolichestvoDnei");

    if (Date1.value && Date2.value && Date2.value.getTime() > Date1.value.getTime()) {
        let Count = ((Date2.value.getTime() - Date1.value.getTime()) / (1000 * 60 * 60 * 24));
        CountDays.value = Count + 1;
    }

}

export async function CheckNumber(sender: TextArea, e: any): JQueryDeferred<void> {  //Контроль длины телефона

    let layout = sender.layout;
    let Number = layout.controls.tryGet<TextArea>("Telefon");

    if (Number.value.length > 12) {
        Number.value = Number.value.slice(0, 12);
    }
}

export async function Info(sender: CustomButton, e: any): JQueryDeferred<void> { //Краткая информация

    let layout = sender.layout;
    let Numerator = layout.controls.tryGet<Numerator>("Numerator");
    let DateCreate = layout.controls.tryGet<DateTimePicker>("DateReg");
    let Date1 = layout.controls.tryGet<DateTimePicker>("DataKomandirovkiOt");
    let Date2 = layout.controls.tryGet<DateTimePicker>("DataPo");
    let reason = layout.controls.tryGet<TextArea>("Osnovanie");

    if (Numerator && DateCreate && Date1 && Date2 && reason) {
        let message = "Номер заявки: " + Numerator.params.value.number +
            "\nДата создания: " + DateCreate.params.value.toLocaleDateString() +
            "\nДаты командировки С: " + Date1.params.value.toLocaleDateString() +
            "\nпо: " + Date2.params.value.toLocaleDateString() +
            "\nОснование: " + reason.params.value.toString();
        alert(message);
    }
}

export async function CheckForSave(sender: Layout, e: CancelableEventArgs<ICardSavingEventArgs>): JQueryDeferred<void> { //Проверка перед соханением

    let layout = sender.layout;
    let Name = layout.controls.tryGet<TextArea>("Name");

    if (Name.value == null) {
        e.cancel();
        MessageBox.ShowWarning("Сохранение отклонено");
        return;
    }
}

export async function Test(sender: CustomButton, e: any): JQueryDeferred<void> {  // тест серверного расширения

    let text = await $OneController.Test();
    alert(text);
}

export async function GetEmployeeData(sender: Employee, e: any): JQueryDeferred<void> {  // заполнение полей "руководитель" и "телефон"

    let layout = sender.layout;
    let PhoneControl = layout.controls.tryGet<TextBox>("Telefon");
    let ManagerControl = layout.controls.tryGet<Employee>("Rukovoditel");

    if (!sender.hasValue()) {
        PhoneControl.value = null;
        ManagerControl.value = null;
        return;
    }

    let model = await $OneController.GetEmployeeData(sender.value.id);

    if (model) {
        PhoneControl.value = model.phone;
        ManagerControl.value = model.manager;
    }

}

export async function GetGroupData(sender: Layout, e: any): JQueryDeferred<void> { // заполнение поля оформляющего

    let layout = sender.layout;
    let Issuing = layout.controls.tryGet<MultipleEmployees>("KtoOformlyaet");

    let model = await $OneController.GetGroupData();

    if (model) {
        Issuing.value = model.groupEmployees;
    }
}

export async function GetAllowanceData(sender: DirectoryDesignerRow, e: any): JQueryDeferred<void> { // сумма командировочных

    let layout = sender.layout;
    let CountDays = layout.controls.tryGet<NumberControl>("KolichestvoDnei");
    let DailyAllowance = layout.controls.tryGet<NumberControl>("SummaKomandirov");
    let model = await $OneController.GetAllowanceData(sender.value.id);

    if (model) {
        DailyAllowance.value = model.dailyAllowance * CountDays.value;
    }
}

export async function ChangeState(sender: CustomButton, e: any): JQueryDeferred<void> { // на согласование

    let layout = sender.layout;
    let model = await $OneController.ChangeState();

    if (model) {
        layout.changeState(model.needState);
        sender.params.visibility = false;
    }

}

export async function CheckState(sender: Layout, e: any): JQueryDeferred<void> { // проверка для отображения кнопки 

    let layout = sender.layout;
    let needButton = layout.controls.tryGet<CustomButton>("OnApproval");

    if (layout.cardInfo.state.caption != "Проект") {
        needButton.params.visibility = false;
    }
}

export async function GetTicketCost(sender: CustomButton, e: any): JQueryDeferred<void> { // стоимость билетов

    let layout = sender.layout;
    let data1 = layout.controls.tryGet<DateTimePicker>("DataKomandirovkiOt");
    let data2 = layout.controls.tryGet<DateTimePicker>("DataPo");
    let city = layout.controls.tryGet<DirectoryDesignerRow>("Gorod");
    let cost = layout.controls.tryGet<NumberControl>("Cost");

    let model = await $OneController.GetTicketCost(city.value.id, data1.value, data2.value);

    if (model) {
        cost.value = model.totalValue;
    }
}
