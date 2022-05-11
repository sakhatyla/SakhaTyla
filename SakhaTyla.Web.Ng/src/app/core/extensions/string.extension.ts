String.prototype.capitalizeFirstLetter = function(this: string): string {
  return this.charAt(0).toUpperCase() + this.slice(1);
};

String.prototype.lowerFirstLetter = function(this: string): string {
  return this.charAt(0).toLowerCase() + this.slice(1);
};
