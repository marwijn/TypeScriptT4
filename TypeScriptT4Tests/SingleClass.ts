module Module3 {
    export interface Interface1 {
        getNumber(input: number): number;
    }
}

module Module2.Module1 {
    export class Class1 {
        constructor (parameter2: Module3.Interface1) { }
        function1(parameter1: string): void { };
    }
}