describe('angular roomie.devices getNewModeToToggleSetpoint (factory)', function () {

  var $injector;

  beforeEach(angular.mock.module('roomie.devices'));

  beforeEach(angular.mock.inject(function (_$injector_) {
    $injector = _$injector_;
  }));

  it('toggles valid values', function () {
    var subject = $injector.get('getNewModeToToggleSetpoint');

    expect(subject('off', 'cool')).toBe('cool');
    expect(subject('off', 'heat')).toBe('heat');

    expect(subject('cool', 'cool')).toBe('off');
    expect(subject('cool', 'heat')).toBe('auto');

    expect(subject('heat', 'cool')).toBe('auto');
    expect(subject('heat', 'heat')).toBe('off');

    expect(subject('auto', 'cool')).toBe('heat');
    expect(subject('auto', 'heat')).toBe('cool');
  });

  it('returns undefined for invalid values', function () {
    var subject = $injector.get('getNewModeToToggleSetpoint');

    expect(subject()).toBeUndefined();

    expect(subject('', '')).toBeUndefined();
    expect(subject('off', '')).toBeUndefined();
    expect(subject('', 'cool')).toBeUndefined();

    expect(subject('derp', 'derp')).toBeUndefined();
    expect(subject('off', 'derp')).toBeUndefined();
    expect(subject('derp', 'cool')).toBeUndefined();

    expect(subject(undefined, undefined)).toBeUndefined();
    expect(subject('off', undefined)).toBeUndefined();
    expect(subject(undefined, 'cool')).toBeUndefined();
  });

});
