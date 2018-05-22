import {Pipe, PipeTransform} from '@angular/core';

@Pipe({name: "orderby"})
export class OrderByPipe implements PipeTransform {
	transform(array: Array<string>, args: string, isNumber: boolean): Array<string> {
		if (array !== undefined) {
			array.sort((a: any, b: any) => {
				let leftValue = a[args];
				let rightValue = b[args];
				if (isNumber) {
					leftValue = parseFloat(leftValue);
					rightValue = parseFloat(rightValue);
				}

				if (leftValue < rightValue) {
					return -1;
				} else if (leftValue > rightValue) {
					return 1;
				} else {
					return 0;
				}
			});
		}
		return array;
	}
}