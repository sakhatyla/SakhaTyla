export class ConvertStringTo {
  static string(value: string): string {
    return value;
  }
  static number(value: string): number {
    return +value;
  }
  static boolean(value: string): boolean {
    return value === 'true';
  }
  static Date(value: string): Date {
    return new Date(value);
  }
}
